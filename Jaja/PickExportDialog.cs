using System;
using System.IO;
using System.Windows.Forms;

namespace Jaja
{
    public partial class PickExportDialog : Form
    {
        public bool ExportExcel1 { get { return chbItemsCSV.Checked; } }
        public bool ExportExcel2 { get { return chbProjectsCSV.Checked; } }
        public bool ExportHTML1 { get { return chbItemsHTML.Checked; } }
        public bool ExportHTML2 { get { return chbProjectsHTML.Checked; } }
        public string Folder {  get { return tbFolder.Text; } }
        public string File { get { return tbFile.Text; } }

        public PickExportDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void tbFolder_TextChanged(object sender, EventArgs e)
        {
            btOK.Enabled = Directory.Exists(tbFolder.Text) && !string.IsNullOrWhiteSpace(tbFile.Text);
        }
    }
}   