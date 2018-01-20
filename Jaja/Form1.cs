using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jaja
{
    public partial class Form1 : Form
    {
        private static bool AllItemsReady = false;

        List<MAPIFolderInfo> AllMAPIFolders = new List<MAPIFolderInfo>();
        List<AppointmentInfo> AllAppointments = new List<AppointmentInfo>();
        ListViewItem lvTotal = new ListViewItem("Celkem");

        bool showProjectsOnly = false;
        bool ShowProjectsOnly
        {
            get { return showProjectsOnly; }
            set
            {
                if (ShowProjectsOnly != value)
                {
                    showProjectsOnly = value;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            btExport.Enabled = true;

            AllAppointments.Clear();
            lvAppointments.Items.Clear();

            Process();
        }

        private void Process()
        {
            if (AllMAPIFolders.Count == 0)
            {
                NameSpace ns;
                Microsoft.Office.Interop.Outlook.Application o = new Microsoft.Office.Interop.Outlook.Application();
                ns = o.GetNamespace("MAPI");
                var mapiFolder = new MAPIFolderInfo(ns.GetDefaultFolder(OlDefaultFolders.olFolderCalendar));

                AddMAPIFolderToList(mapiFolder);
            }
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ProcessFolders();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private static List<MAPIFolder> SelectFolders()
        {
            List<MAPIFolder> folders = new List<MAPIFolder>();

            Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.NameSpace session = app.Session;

            foreach (MAPIFolder f in session.Folders)
                folders.Add(f);
            return folders;
        }

        private void ProcessFolders()
        {
            AllAppointments = LoadAllData();
            AllAppointments.Sort((a, b) => a.ToString().CompareTo(b.ToString()));
            ShowData();
        }

        private void SetStatus(string label, int? val)
        {
            if (label != null)
            {
                toolStripStatusLabel1.Text = label;
            }
            if (val.HasValue)
            {
                toolStripProgressBar1.Visible = val.Value >= toolStripProgressBar1.Minimum && val.Value <= toolStripProgressBar1.Maximum;
                if (toolStripProgressBar1.Visible)
                {
                    toolStripProgressBar1.Value = val.Value;
                }
            }
        }

        private void ShowData()
        {
            Log.Write($"ShowData: ");
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Log.Write($"ShowData: loading projects...");
                IEnumerable<IGrouping<string, AppointmentInfo>> projects = AllAppointments.GroupBy(x => x.Project);
                Log.Write($"ShowData: projects loaded");
                Log.Write($"ShowData: {projects.Count()}");

                lvAppointments.BeginUpdate();
                lvAppointments.Items.Clear();

                foreach (var project in projects)
                {
                    Log.Write($"ShowData: processing project {project.Key}");
                    ProjectInfo projectInfo = new ProjectInfo(project.Key);

                    TimeSpan allTimes = new TimeSpan(0);

                    ListViewItem li1 = new ListViewItem(projectInfo.Name);
                    li1.Font = new System.Drawing.Font(li1.Font, FontStyle.Bold);
                    Log.Write($"ShowData: counting duration...");
                    TimeSpan duration = Tools.GetDuration(AllAppointments, projectInfo.Name);
                    Log.Write($"ShowData: duration is {duration}");

                    li1.SubItems.Add(duration.TotalHours.ToString());
                    li1.Checked = true;
                    li1.Tag = projectInfo;
                    lvAppointments.Items.Add(li1);
                    allTimes += duration;

                    TimeSpan tsTotal = new TimeSpan(0);
                    foreach (AppointmentInfo ai in project.OrderBy(d => d.Appointment.Start))
                    {
                        Log.Write($"ShowData: processing appointment {ai.Subject}");
                        // ai.ProjectInfo = projectInfo;
                        TimeSpan ts = ai.Appointment.End - ai.Appointment.Start;
                        tsTotal += ts;
                        if (!ShowProjectsOnly)
                        {
                            ListViewItem li = new ListViewItem(ai.Subject);
                            li.SubItems.Add(ts.TotalHours.ToString());
                            li.SubItems.Add(ai.Appointment.Start.Date.ToShortDateString());
                            li.SubItems.Add(ai.FolderInfo.ToString());
                            li.Checked = ai.Appointment.Sensitivity == 0;
                            li.Tag = ai;
                            lvAppointments.Items.Add(li);
                        }
                    }
                }

                lvTotal.Font = new System.Drawing.Font(lvTotal.Font, FontStyle.Bold);
                lvTotal.SubItems.Add(Tools.GetDuration(AllAppointments).TotalHours.ToString());
                lvTotal.Checked = false;
                lvTotal.BackColor = Color.Red;
                lvAppointments.Items.Add(lvTotal);
            }
            finally
            {
                lvAppointments.EndUpdate();
                AllItemsReady = true;
                this.Cursor = Cursors.Default;
            }
        }

        private List<AppointmentInfo> LoadAllData()
        {
            List<AppointmentInfo> allData = new List<AppointmentInfo>();
            Log.Write($"allData cleared.");
            try
            {
                foreach (var folder in AllMAPIFolders)
                {
                    Log.Write($"LoadAllData: {folder.Folder.FullFolderPath}");
                    try
                    {
                        SetStatus(folder.ToString(), 0);

                        Items items = folder.Folder.Items;
                        if (items == null)
                        {
                            Log.Write($"LoadAllData: Items in folder {folder.Folder.FullFolderPath} is null!");
                        }

                        items.IncludeRecurrences = true;

                        Log.Write($"LoadAllData: Total {items.Count} items");

                        int n = 0;
                        int lastProgress = 0;
                        int p = 0;

                        foreach (var item in items)
                        {
                            n += 1;
                            p = 100 * n / items.Count;
                            if (p != lastProgress)
                            {
                                SetStatus(null, p);
                                lastProgress = p;
                            }

                            AppointmentItem ai = item as AppointmentItem;
                            if (ai == null)
                                continue;

                            //Log.Write($"LoadAllData: Processing item {n} - {ai.Subject}");

                            if (ai.Sensitivity != 0)
                            {
                                //    Log.Write("Skipping private event");
                                continue;
                            }

                            if (ai.IsRecurring)
                            {
                                //  Log.Write($"LoadAllData: {ai.Subject} is recurring...");
                                Microsoft.Office.Interop.Outlook.RecurrencePattern rp = ai.GetRecurrencePattern();
                                Microsoft.Office.Interop.Outlook.AppointmentItem recur = null;
                                for (DateTime cur = monthCalendar1.SelectionStart.AddHours(ai.Start.Hour).AddMinutes(ai.Start.Minute); cur < monthCalendar2.SelectionStart.AddDays(1); cur = cur.AddDays(1))
                                {
                                    try
                                    {
                                        recur = rp.GetOccurrence(cur);
                                        //Log.Write($"LoadAllData: Adding occurence of recurring item {ai.Subject} for date {cur}");
                                        allData.Add(new AppointmentInfo(recur, folder));
                                    }
                                    catch
                                    { }
                                }
                            }
                            else
                            {
                                if ((ai.Start >= monthCalendar1.SelectionStart) && (ai.End < monthCalendar2.SelectionStart.AddDays(1)))
                                {
                                    //Log.Write($"LoadAllData: Adding item {ai.Subject}");
                                    allData.Add(new AppointmentInfo(ai, folder));
                                }
                                else
                                {
                                    //Log.Write($"LoadAllData: {ai.Subject} is out of date frame, continuing...");
                                    continue;
                                }
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Log.Write($"[EXCEPTION] Nastala výjimka při zpracování složky {folder.Folder.Name}:\r\n {ex.Message}");
                        MessageBox.Show($"Nastala výjimka při zpracování složky {folder.Folder.Name}:\r\n {ex.Message}", "Výjimka", MessageBoxButtons.OK);
                    }
                    finally
                    {
                        folder.WasProcessed = true;
                    }
                }
            }
            finally
            {
                SetStatus("Načteno", -1);
            }
            return allData;
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            PickExportDialog pe = new PickExportDialog();
            if (pe.ShowDialog() == DialogResult.OK)
            {
                if (pe.ExportHTML1)
                {
                    ExportItemsHtml(Path.Combine(pe.Folder, pe.File + " (po položkách).htm"));
                }

                if (pe.ExportHTML2)
                {
                    ExportProjectsHtml(Path.Combine(pe.Folder, pe.File + " (po projektech).htm"));
                }

                if (pe.ExportExcel1)
                {
                    ExportItemsCsv(Path.Combine(pe.Folder, pe.File + " (po položkách).csv"));
                }

                if (pe.ExportExcel2)
                {
                    ExportProjectsCsv(Path.Combine(pe.Folder, pe.File + " (po projektech).csv"));
                }

                MessageBox.Show("Hotovo.");
            }
        }

        private TimeSpan ExportProjectsHtml(string fileName)
        {
            Log.Write($"Export projects HTML");
            TimeSpan ts = new TimeSpan(0);
            using (StreamWriter w = new StreamWriter(fileName))
            {
                w.WriteLine(@"<html><head><meta charset='UTF-8'><title>Práce po projektech</title>
                                <style>
                            td { padding: 0px 5px 0px 5px; border: none; }
                            td.bl { border-left: 1px solid gray; }
                            td.br { border-right: 1px solid gray; }
                            td.bt { border-top: 1px solid gray; }
                            td.bb { border-bottom: 1px solid gray; }
                            </style></head><body>");
                w.WriteLine("<table cellspacing=0><tr><td style='font-weight:bold'>PROJEKT</td><td style='font-weight:bold'>KALENDÁŘ</td><td style='font-weight:bold; text-align:right'>ČAS</td></tr>");
                var projects = AllAppointments.Where(a => a.Enabled).GroupBy(x => x.Project);
                string projekt = "";
                bool projectWrite = false;
                TimeSpan totalTime = TimeSpan.FromSeconds(0);
                Log.Write($"Export Projects HTML, total time is now {totalTime}");
                foreach (var project in projects)
                {                    
                    projekt = project.Key;
                    projectWrite = true;
                    Log.Write($"Export Projects HTML, project {projekt}, total time is now {totalTime}");
                    foreach (var apkal in AllAppointments.Where(x => x.Enabled && x.ProjectInfo.Name == project.Key).GroupBy(k => GetFolderPath(k.FolderInfo.Folder.FullFolderPath)))
                    {
                        TimeSpan d = TimeSpan.FromSeconds(AllAppointments.Where(a => a.Enabled && a.ProjectInfo.Name == project.Key && GetFolderPath(a.FolderInfo.Folder.FullFolderPath) == apkal.Key).Sum(i => (i.Appointment.End - i.Appointment.Start).TotalSeconds));
                        Log.Write($"Export Projects HTML, project {projekt} duration is {d}");
                        totalTime += d;
                        Log.Write($"Export Projects HTML, total time is now {totalTime}");
                        string str = string.Format("{0}h {1}m", (int)d.TotalHours, d.Minutes.ToString().PadLeft(2, '0'));
                        string proj = projectWrite ? projekt : string.Empty;

                        string classTop = projectWrite ? "bt" : "";
                        string classStr = $"class='bl {classTop}'";

                        w.WriteLine($"<tr><td {classStr}><b>{proj}</b></td><td  {classStr}><i>{apkal.Key}</i></td><td {classStr} style='text-align:right; border-right: 1px solid gray'>{str}</td></tr>");
                        projectWrite = false;
                    }
                    w.WriteLine($"<tr><td class='bl bb'>&nbsp;</td><td class='bl bt bb'><b><i>PROJEKT CELKEM</i></b></td><td class='bl bt bb' style='text-align:right; border-right: 1px solid gray'><b><i>{Tools.GetDuration(AllAppointments, projekt)}</i></b></td></tr>");
                }

                w.WriteLine($"<tr><td class='bl bt bb'><b>CELKEM</b></td><td class='bl bt bb'></td><td class='bl bt bb' style='text-align:right; font-weight:bold; border-right: 1px solid gray'>{(int)totalTime.TotalHours}h {totalTime.Minutes}m</td></tr>");
                w.WriteLine("</table>");
                w.WriteLine("</body></html>");
            }

            return ts;
        }

        private TimeSpan ExportProjectsCsv(string fileName)
        {
            TimeSpan ts = new TimeSpan(0);
            using (StreamWriter w = new StreamWriter(fileName, false, Encoding.Default))
            {
                w.WriteLine(@"PROJEKT;KALENDÁŘ;ČAS");
                var projects = AllAppointments.Where(a => a.Enabled).GroupBy(x => x.Project);

                foreach (var project in projects)
                {
                    foreach (var apkal in AllAppointments.Where(x => x.Enabled && x.ProjectInfo.Name == project.Key).GroupBy(k => GetFolderPath(k.FolderInfo.Folder.FullFolderPath)))
                    {
                        TimeSpan d = TimeSpan.FromSeconds(AllAppointments.Where(a => a.Enabled && a.ProjectInfo.Name == project.Key && GetFolderPath(a.FolderInfo.Folder.FullFolderPath) == apkal.Key).Sum(i => (i.Appointment.End - i.Appointment.Start).TotalSeconds));
                        w.WriteLine($"{project.Key};{apkal.Key};{d}");
                    }
                }
            }

            return ts;
        }

        private TimeSpan ExportItemsHtml(string fileName)
        {
            TimeSpan ts = new TimeSpan(0);
            Log.Write($"Export Items HTML, total time is now {ts}");
            using (StreamWriter w = new StreamWriter(fileName))
            {
                w.WriteLine("<html><head><meta charset=\"UTF-8\"><title>Export z Outlooku</title></head><body><table cellspacing=0><tr><td style=\"font-weight:bold;width:480px;padding-right:50px\">Projekt</td><td style=\"font-weight:bold;width:200px;padding-right:50px;\">Doba</td><td style=\"font-weight:bold;width:200px;\">Datum</td><td style=\"font-weight:bold;width:350px;\">Kalendář</td></tr>");
                foreach (ListViewItem li in lvAppointments.Items)
                {
                    if (!li.Checked)
                        continue;
                    if (li.Tag is ProjectInfo)
                    {
                        ProjectInfo p = li.Tag as ProjectInfo;

                        if (AllAppointments.Any(x => x.Enabled && x.Project == p.Name))
                        {
                            var duration = Tools.GetDuration(AllAppointments, p.Name);
                            if (ShowProjectsOnly)
                            {
                                ts += duration;
                            }
                            w.WriteLine(string.Format("<tr style=\"background-color:lightblue;\"><td><b>{0}</b></td><td><b>{1}</b></td><td><b></b></td><td><b></b></td></tr>", p.Name, duration.TotalHours.ToString()));
                        }
                    }
                    else
                        if (li.Tag is AppointmentInfo)
                    {
                        AppointmentInfo pi = li.Tag as AppointmentInfo;
                        TimeSpan duration = pi.Appointment.End - pi.Appointment.Start;
                        w.WriteLine(string.Format("<tr><td><i>{0}</i></td><td><i>{1}</i></td><td><i>{2}</i></td><td><i>{3}</i></td></tr>", pi.Subject, duration.TotalHours.ToString(), pi.Appointment.Start.ToShortDateString(), pi.FolderInfo.ToString()));                        
                        ts += duration;
                        Log.Write($"Export Items HTML, duration of {pi.Appointment.Subject} is {duration}, total is {ts}");
                    }
                }

                w.WriteLine(string.Format("<tr style=\"background-color:red\"><td><b><i>{0}</i></b></td><td><b><i>{1}</i></b></td><td><b><i></i></b></td><td><b></b></td></tr>", "CELKEM", ts.TotalHours.ToString()));
                w.WriteLine("</table></body></html>");
            }

            return ts;
        }

        private TimeSpan ExportItemsCsv(string fileName)
        {
            TimeSpan ts = new TimeSpan(0);
            using (StreamWriter w = new StreamWriter(fileName, false, Encoding.Default))
            {
                w.WriteLine("PROJEKT;POLOŽKA;DOBA;DATUM;KALENDÁŘ");
                Log.Write($"Export CSV Items count: {lvAppointments.Items.Count} ");
                foreach (ListViewItem li in lvAppointments.Items)
                {
                    Log.Write($"Export CSV Items: Item: {li.Text}, CHecked: {li.Checked} ");
                    if (!li.Checked)
                        continue;
                    if (li.Tag is ProjectInfo)
                    {
                        Log.Write($"Skipping project");
                        continue;
                    }
                    else
                    {
                        if (li.Tag is AppointmentInfo)
                        {
                            AppointmentInfo pi = li.Tag as AppointmentInfo;
                            TimeSpan duration = pi.Appointment.End - pi.Appointment.Start;
                            w.WriteLine($"{pi.Project};{pi.Subject};{duration};{pi.Appointment.Start};{pi.FolderInfo}");
                            ts += duration;
                        }
                    }
                }
            }

            return ts;
        }

        private string GetFolderPath(string name)
        {
            return name.Trim('\\').Replace("\\Kalendář", "");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime d1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime d2 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1);
            monthCalendar1.SetDate(d1);
            monthCalendar2.SetDate(d2);

            monthCalendar1_DateChanged(this, new DateRangeEventArgs(monthCalendar1.SelectionStart, monthCalendar1.SelectionEnd));
            btKalendar.Focus();
        }

        private Color checkCorrectDate()
        {
            btLoad.Enabled = monthCalendar2.SelectionStart >= monthCalendar1.SelectionStart;
            return btLoad.Enabled ? Color.Black : Color.Red;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            label1.Text = monthCalendar1.SelectionStart.ToString("dd.MM.yyyy") + " - " + monthCalendar2.SelectionStart.ToString("dd.MM.yyyy");
            label1.ForeColor = checkCorrectDate();
        }


        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (AllItemsReady)
            {
                if (e.Item.Tag is ProjectInfo)
                {
                    processProject(e.Item);
                }
                else if (e.Item.Tag is AppointmentInfo)
                {
                    ((AppointmentInfo)e.Item.Tag).Enabled = e.Item.Checked;
                    processItem(e.Item);
                }
            }
        }

        private void processItem(ListViewItem listViewItem)
        {
            ListViewItem projectItem = null;
            AppointmentInfo aitem = (AppointmentInfo)listViewItem.Tag;

            TimeSpan duration = new TimeSpan(0);

            foreach (ListViewItem item in lvAppointments.Items)
            {
                ProjectInfo pi = item.Tag as ProjectInfo;
                if (pi != null)
                {
                    if (pi.Name == aitem.Project)
                    {
                        projectItem = item;
                        break;
                    }
                }
            }

            foreach (ListViewItem item in lvAppointments.Items)
            {
                if (item.Checked)
                {
                    AppointmentInfo ai = item.Tag as AppointmentInfo;
                    if (ai != null)
                    {
                        if (ai.Project == aitem.Project)
                        {
                            duration += ai.Appointment.End - ai.Appointment.Start;
                        }
                    }
                }
            }
            projectItem.SubItems[1].Text = Tools.GetDuration(AllAppointments, aitem.Project).TotalHours.ToString();

            recomputeTotal();
        }

        private void processProject(ListViewItem listViewItem)
        {
            foreach (ListViewItem item in lvAppointments.Items)
            {
                AppointmentInfo ai = item.Tag as AppointmentInfo;
                if (ai != null)
                {
                    if (ai.Project == ((ProjectInfo)listViewItem.Tag).Name)
                    {
                        item.Checked = listViewItem.Checked;
                        ai.Enabled = item.Checked;
                    }
                }
            }

            TimeSpan duration = Tools.GetDuration(AllAppointments, ((ProjectInfo)listViewItem.Tag).Name);
            listViewItem.SubItems[1].Text = duration.TotalHours.ToString();
            recomputeTotal();
        }

        private void recomputeTotal()
        {
            TimeSpan ts = new TimeSpan(0);
            foreach (ListViewItem item in lvAppointments.Items)
            {
                ProjectInfo p = item.Tag as ProjectInfo;
                if (p != null)
                {
                    ts += Tools.GetDuration(AllAppointments, p.Name);
                }
            }

            lvTotal.SubItems[1].Text = ts.TotalHours.ToString() + "h";
        }

        private void brKalendar_Click(object sender, EventArgs e)
        {
            PickFoldersDialog pd = new PickFoldersDialog();
            pd.AllMAPIFolders = this.AllMAPIFolders;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                lvKalendare.Items.Clear();

                List<MAPIFolderInfo> temp = new List<MAPIFolderInfo>();

                foreach (var f in AllMAPIFolders)
                {
                    if (!pd.SelectedFolders.Any(x => x.ToString() == f.ToString()))
                    {
                        temp.Add(f);
                    }
                }

                foreach (var t in temp)
                {
                    AllMAPIFolders.Remove(t);
                }

                foreach (var f in pd.SelectedFolders)
                {
                    AddMAPIFolderToList(f);
                }
            }
        }

        private void AddMAPIFolderToList(MAPIFolderInfo f)
        {
            ListViewItem item = new ListViewItem(f.ToString())
            {
                Checked = true,
                ToolTipText = f.ToString(),
                Tag = f
            };

            lvKalendare.Items.Add(item);
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            AllAppointments.Clear();
            lvAppointments.Items.Clear();
            lvKalendare.Items.Clear();
            AllMAPIFolders.Clear();
            btKalendar.Enabled = true;
            btLoad.Enabled = true;
        }

        private void lvKalendare_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            MAPIFolderInfo fi = e.Item.Tag as MAPIFolderInfo;
            if (fi != null)
            {
                if ((e.Item.Checked) && (!AllMAPIFolders.Any(f => f.ToString() == fi.ToString())))
                {
                    AllMAPIFolders.Add(fi);
                }
                else
                {
                    if (AllMAPIFolders.Any(f => f.ToString() == fi.ToString()))
                    {
                        AllMAPIFolders.Remove(fi);
                    }
                }

                if (fi.WasProcessed)
                {
                    foreach (ListViewItem li in lvAppointments.Items)
                    {
                        AppointmentInfo ai = li.Tag as AppointmentInfo;
                        if (ai != null)
                        {
                            if (ai.FolderInfo == fi)
                            {
                                li.Checked = e.Item.Checked;
                                ai.Enabled = e.Item.Checked;
                            }
                        }
                    }

                    recomputeTotal();
                }
            }
        }

        private void chbShowProjectsOnly_CheckedChanged(object sender, EventArgs e)
        {
            ShowProjectsOnly = chbShowProjectsOnly.Checked;
            ShowData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string log = Path.GetDirectoryName(Log.File);
            Process proc = new System.Diagnostics.Process();
            ProcessStartInfo psi = new ProcessStartInfo(log);
            proc.StartInfo = psi;
            proc.Start();
        }
    }
}