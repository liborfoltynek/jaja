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
    public partial class Progress0 : Form
    {
        public bool CancelRequest { get; private set; }

        public Progress0()
        {
            InitializeComponent();
        }

        public void SetProgress(int p)
        {
            progressBar1.Value = p;
            Application.DoEvents();   
        }

        public void SetLabel(string s)
        {
            label1.Text = s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CancelRequest = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CancelRequest = true;
        }
    }
}
