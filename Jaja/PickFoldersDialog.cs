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

        public PickFoldersDialog()
        {
            InitializeComponent();
        }

        private void PickFoldersDialog_Load(object sender, EventArgs e)
        {
            
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            foreach (ListViewItem item in chlFolders.CheckedItems)
            {
                selectedFolders.Add((MAPIFolderInfo)item.Tag);
            }
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private  void button1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in chlFolders.Items)
            {
                li.Checked = true;
            }
        }

        private void loadCallendars()
        {
            chlFolders.Items.Clear();

            Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.NameSpace session = app.Session;
            chlFolders.SuspendLayout();
            foreach (MAPIFolder ff in session.Folders)
            {
                foreach (MAPIFolder folder in ff.Folders)
                {
                    if (folder.DefaultMessageClass == "IPM.Appointment")
                    {
                        MAPIFolderInfo folderInfo = new MAPIFolderInfo(folder);
                        ListViewItem li = new ListViewItem(folderInfo.ToString());
                        li.Tag = folderInfo;
                        chlFolders.Items.Add(li);
                    }
                }
            }
            chlFolders.ResumeLayout();
            chlFolders.Refresh();
        }

        private void PickFoldersDialog_Activated(object sender, EventArgs e)
        {
            loadCallendars();
        }
    }
}
