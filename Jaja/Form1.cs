using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jaja
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

               
        ListViewItem lvTotal = new ListViewItem("Celkem");

        private async void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button1.Enabled = false;
            btKalendar.Enabled = false;
            //button1.Visible = false;
            //            monthCalendar1.Visible = false;
            //            monthCalendar2.Visible = false;
            monthCalendar1.Enabled = false;
            monthCalendar2.Enabled = false;

            await Process();
        }

        List<MAPIFolderInfo> folders = new List<MAPIFolderInfo>();

        private async Task Process()
        {

            // MAPIFolder folder = SelectFolder();

            if (folders.Count == 0)
            {
                NameSpace ns;
                Microsoft.Office.Interop.Outlook.Application o = new Microsoft.Office.Interop.Outlook.Application();
                ns = o.GetNamespace("MAPI");
                folders.Add(new MAPIFolderInfo(ns.GetDefaultFolder(OlDefaultFolders.olFolderCalendar)));
            }
            this.Cursor = Cursors.WaitCursor;
            try
            {
                await ProcessFolder();
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

        private async Task ProcessFolders()
        {
            List<AppointmentInfo> AllAppointments = await LoadAllData();

            var projects = AllAppointments.GroupBy(x => x.Project);
            foreach (var project in projects)
            {
                string projectName = project.Key;

                foreach (var ai in  project.OrderBy(d=>d.Appointment.Start).ToList())
                {
                    
                }
            }

        }

        private async Task<List<AppointmentInfo>> LoadAllData()
        {
            List<AppointmentInfo> allData = new List<AppointmentInfo>();

            foreach (var folder in folders)
            {
                Items items = folder.Folder.Items;
                string all = items.Count.ToString();

                foreach (var item in items)
                {
                    AppointmentItem ai = item as AppointmentItem;
                    if (ai == null)
                        continue;

                    if ((ai.Start < monthCalendar1.SelectionStart) || (ai.Start >= monthCalendar2.SelectionStart.AddDays(1)))
                        continue;

                    allData.Add(new AppointmentInfo(ai, folder));
                }                                
            }

            return allData;
        }

        /// <summary>
        /// hlavni metoda
        /// </summary>
        /// <returns></returns>
        private async Task ProcessFolder()
        {
            TimeSpan allTimes = new TimeSpan(0);
            int cnt = 0;
            Dictionary<string, Project> AllProjects = new Dictionary<string, Project>();
            Dictionary<string, List<AppointmentItem>> projects = new Dictionary<string, List<AppointmentItem>>();
            this.Cursor = Cursors.WaitCursor;
            foreach (var folder in folders)
            {
                Items items = folder.Folder.Items;

                string all = items.Count.ToString();
                //listView1.SuspendLayout();
                ListViewItem[] list = new ListViewItem[items.Count];
                cnt += await ProcessAppointments(folder.Folder.FolderPath, items, projects, cnt, all, AllProjects);
            }

            Dictionary<string, TimeSpan> times = new Dictionary<string, TimeSpan>();
            allTimes += await ProcessItems(AllProjects, times, allTimes);

            lvTotal.Font = new System.Drawing.Font(lvTotal.Font, FontStyle.Bold);
            lvTotal.SubItems.Add(allTimes.TotalHours.ToString());
            lvTotal.Checked = false;
            lvTotal.BackColor = Color.Red;
            listView1.Items.Add(lvTotal);

            listView1.ResumeLayout();
            AllItemsReady = true;
        }

        private async Task<int> ProcessAppointments(string folderPath, Items items, Dictionary<string, List<AppointmentItem>> projects, int cnt, string all, Dictionary<string, Project> AllProjects)
        {
            foreach (object obj in items)
            {
                AppointmentItem ai = obj as AppointmentItem;
                if (ai == null)
                    continue;

                if ((ai.Start < monthCalendar1.SelectionStart) || (ai.Start >= monthCalendar2.SelectionStart.AddDays(1)))
                    continue;
                cnt += 1;
                this.Text = string.Format("{0} of {1}", cnt, all);

                //ListViewItem li = new ListViewItem(cnt.ToString());
                //li.Checked = ai.Sensitivity == 0;

                string project = GetProjectName(ai.Subject);
                if (!projects.ContainsKey(project))
                {
                    projects.Add(project, new List<AppointmentItem>());
                    AllProjects.Add(project, new Project(project));
                }

                projects[project].Add(ai);

                AllProjects[project].AddItem(new ProjectItem(project, ai.Subject, ai.Start, ai.End) { Enabled = ai.Sensitivity == 0 });
            }

            return cnt;
        }

        private async Task<TimeSpan> ProcessItems(Dictionary<string, Project> AllProjects, Dictionary<string, TimeSpan> times, TimeSpan allTimes)
        {
            foreach (Project project in AllProjects.Values)
            {
                times.Add(project.ProjectName, new TimeSpan(0));
                TimeSpan tsTotal = new TimeSpan(0);

                ListViewItem li1 = new ListViewItem(project.ProjectName);
                li1.Font = new System.Drawing.Font(li1.Font, FontStyle.Bold);
                li1.SubItems.Add(project.GetDuration().TotalHours.ToString());
                li1.Checked = true;
                li1.Tag = project;
                listView1.Items.Add(li1);
                allTimes += project.GetDuration();

                project.list.Sort(Tools.CompareItems);

                foreach (ProjectItem ai in project.list)
                {
                    TimeSpan ts = ai.End - ai.Start;
                    tsTotal += ts;
                    ListViewItem li = new ListViewItem(ai.Subject);
                    li.SubItems.Add(ts.TotalHours.ToString());
                    li.SubItems.Add(ai.Start.Date.ToShortDateString());
                    li.Checked = ai.Enabled;
                    li.Tag = ai;
                    listView1.Items.Add(li);
                }
            }

            return allTimes;
        }



        private string GetProjectName(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
                return "Neidentifikovatelný projekt";

            if (!subject.Contains(' ') && !subject.Contains('-'))
                return subject.Trim();
            int i1 = subject.IndexOf(' ');
            int i2 = subject.IndexOf('-');
            int i = 0;
            if (i1 == -1)
                i = i2;
            else if (i2 == -1)
                i = i1;
            else if (i1 < i2)
                i = i1;
            else i = i2;
            return subject.Substring(0, i).Trim().ToUpper();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "HTML File|*.htm|All Files|*.*";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                TimeSpan ts = new TimeSpan(0);
                using (StreamWriter w = new StreamWriter(sf.FileName))
                {
                    w.WriteLine("<html><head><meta charset=\"UTF-8\"><title>Export z Outlooku</title></head><body><table cellspacing=0><tr><td style=\"font-weight:bold;width:480px;padding-right:50px\">Projekt</td><td style=\"font-weight:bold;width:200px;padding-right:50px;\">Doba</td><td style=\"font-weight:bold;width:200px;\">Datum</td></tr>");
                    foreach (ListViewItem li in listView1.Items)
                    {
                        if (!li.Checked)
                            continue;
                        if (li.Tag is Project)
                        {
                            Project p = li.Tag as Project;
                            if (p.AnyEnabled)
                                w.WriteLine(string.Format("<tr style=\"background-color:lightblue;\"><td><b>{0}</b></td><td><b>{1}</b></td><td><b></b></td></tr>", p.ProjectName, p.GetDuration().TotalHours.ToString()));
                        }
                        else
                            if (li.Tag is ProjectItem)
                        {
                            ProjectItem pi = li.Tag as ProjectItem;
                            w.WriteLine(string.Format("<tr><td><i>{0}</i></td><td><i>{1}</i></td><td><i>{2}</i></td></tr>", pi.Subject, pi.Duration.TotalHours.ToString(), pi.Start.Date.ToShortDateString()));
                            ts += pi.Duration;
                        }
                    }

                    w.WriteLine(string.Format("<tr style=\"background-color:red\"><td><b><i>{0}</i></b></td><td><b><i>{1}</i></b></td><td><b><i></i></b></td></tr>", "CELKEM", ts.TotalHours.ToString()));
                    w.WriteLine("</table></body></html>");
                }
                Process proc = new Process();
                proc.StartInfo = new ProcessStartInfo(sf.FileName);
                proc.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime d1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime d2 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1);
            monthCalendar1.SetDate(d1);
            monthCalendar2.SetDate(d2);

            monthCalendar1_DateChanged(this, new DateRangeEventArgs(monthCalendar1.SelectionStart, monthCalendar1.SelectionEnd));
            monthCalendar2_DateChanged(this, new DateRangeEventArgs(monthCalendar2.SelectionStart, monthCalendar2.SelectionEnd));
        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
            label2.Text = "Do: " + e.Start.ToString("dd.MM.yyyy");
            checkCorrectDate();
            if (!button1.Enabled)
                label2.ForeColor = Color.Red;
            else
            {
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
            }
        }

        private void checkCorrectDate()
        {
            button1.Enabled = monthCalendar2.SelectionStart > monthCalendar1.SelectionStart;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            label1.Text = "Od: " + e.Start.ToString("dd.MM.yyyy");
            checkCorrectDate();
            if (!button1.Enabled)
                label1.ForeColor = Color.Red;
            else
            {
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
            }
        }

        private static bool AllItemsReady = false;

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (AllItemsReady)
            {
                if (e.Item.Tag is Project)
                {
                    processProject(e.Item);
                }
                else if (e.Item.Tag is ProjectItem)
                {
                    ((ProjectItem)e.Item.Tag).Enabled = e.Item.Checked;
                    processItem(e.Item);
                }
            }
        }

        private void processItem(ListViewItem listViewItem)
        {
            ListViewItem projectItem = null;
            ProjectItem pitem = (ProjectItem)listViewItem.Tag;
            foreach (ListViewItem item in listView1.Items)
            {
                Project pi = item.Tag as Project;
                if (pi != null)
                {
                    if (pi.ProjectName == pitem.Project)
                    {
                        projectItem = item;
                        break;
                    }
                }
            }
            projectItem.SubItems[1].Text = ((Project)projectItem.Tag).GetDuration().TotalHours.ToString();
            recomputeTotal();
        }

        private void processProject(ListViewItem listViewItem)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                ProjectItem pi = item.Tag as ProjectItem;
                if (pi != null)
                {
                    if (pi.Project == ((Project)listViewItem.Tag).ProjectName)
                    {
                        item.Checked = listViewItem.Checked;
                        pi.Enabled = item.Checked;
                    }
                }
            }

            listViewItem.SubItems[1].Text = ((Project)listViewItem.Tag).GetDuration().TotalHours.ToString();
            recomputeTotal();
        }



        private void recomputeTotal()
        {
            TimeSpan ts = new TimeSpan(0);
            foreach (ListViewItem item in listView1.Items)
            {
                Project p = item.Tag as Project;
                if (p != null)
                {
                    ts += p.GetDuration();
                }
            }
            lvTotal.SubItems[1].Text = ts.TotalHours.ToString() + "h";
        }

        private void brKalendar_Click(object sender, EventArgs e)
        {
            PickFoldersDialog pd = new PickFoldersDialog();
            if (pd.ShowDialog() == DialogResult.OK)
            {
                folders.Clear();
                lbKalendare.Items.Clear();

                foreach (var f in pd.SelectedFolders)
                {
                    lbKalendare.Items.Add(f);
                    folders.Add(f);
                }
            }          
        }
      
    }
}
