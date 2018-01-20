using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jaja
{
    public partial class PickFoldersDialog : Form
    {
        private List<MAPIFolderInfo> selectedFolders = new List<MAPIFolderInfo>();
        public List<MAPIFolderInfo> SelectedFolders
        {
            get { return selectedFolders.ToList(); }
        }

        public List<MAPIFolderInfo> AllMAPIFolders { get; set; }

        public PickFoldersDialog()
        {
            InitializeComponent();
        }

        private void PickFoldersDialog_Load(object sender, EventArgs e)
        {
            Log.Write("PickFoldersDialog load");
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            foreach (ListViewItem item in chlFolders.CheckedItems)
            {
                MAPIFolderInfo mapi = item.Tag as MAPIFolderInfo;
                if (mapi == null)
                {
                    Log.Write($"Cannot convert folder to mapi: {item.Tag}");
                }
                Log.Write($"Adding MAPI to selected folders: {mapi.ToString()}");
                selectedFolders.Add(mapi);
            }
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Log.Write($"PickFolderDialog cancelled.");
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Log.Write("Selecting all folders.");
            foreach (ListViewItem li in chlFolders.Items)
            {
                li.Checked = true;
            }
        }

        private void loadCallendars()
        {
            Log.Write("Loading callendars...");
            chlFolders.Items.Clear();

            Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.NameSpace session = app.Session;
            chlFolders.BeginUpdate();

            foreach (MAPIFolder ff in session.Folders)
            {
                Log.Write($"Folder in session: {ff}");
                foreach (MAPIFolder folder in ff.Folders)
                {
                    Log.Write($"Folder {folder.FullFolderPath}");
                    if (folder.DefaultMessageClass == "IPM.Appointment")
                    {
                        Log.Write($"Folder {folder.FullFolderPath} is IPM.Appointment");
                        Log.Write($"Creating FolderInfo");
                        MAPIFolderInfo folderInfo = new MAPIFolderInfo(folder);
                        ListViewItem li = new ListViewItem(folderInfo.ToString());
                        Log.Write($"Created ListViewItem: {folderInfo}");
                        li.Tag = folderInfo;
                        bool check = AllMAPIFolders.Any(f => f.ToString() == folderInfo.ToString());
                        Log.Write($"ListView checked status will be: {check}");
                        li.Checked = check;
                        chlFolders.Items.Add(li);
                    }
                    else
                    {
                        Log.Write($"Folder {folder.FullFolderPath} is NOT IPM.Appointment");
                    }
                }
            }
            chlFolders.EndUpdate();
            chlFolders.Refresh();
        }

        bool calLoaded = false;
        private void PickFoldersDialog_Activated(object sender, EventArgs e)
        {
            if (!calLoaded)
            {
                Log.Write("Loading callendars first time...");
                loadCallendars();
                calLoaded = true;
            }
            else
            {
                Log.Write("Callendars already loaded.");
            }
        }
    }
}

