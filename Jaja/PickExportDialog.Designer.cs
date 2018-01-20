namespace Jaja
{
    partial class PickExportDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btFind = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbItemsHTML = new System.Windows.Forms.CheckBox();
            this.chbProjectsHTML = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chbProjectsCSV = new System.Windows.Forms.CheckBox();
            this.chbItemsCSV = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Místo uložení:";
            // 
            // btFind
            // 
            this.btFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btFind.Location = new System.Drawing.Point(395, 21);
            this.btFind.Name = "btFind";
            this.btFind.Size = new System.Drawing.Size(75, 23);
            this.btFind.TabIndex = 1;
            this.btFind.Text = "Najít";
            this.btFind.UseVisualStyleBackColor = true;
            this.btFind.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbFolder
            // 
            this.tbFolder.Location = new System.Drawing.Point(102, 21);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(287, 20);
            this.tbFolder.TabIndex = 0;
            this.tbFolder.TextChanged += new System.EventHandler(this.tbFolder_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chbProjectsHTML);
            this.groupBox1.Controls.Add(this.chbItemsHTML);
            this.groupBox1.Location = new System.Drawing.Point(16, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 89);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HTML";
            // 
            // chbItemsHTML
            // 
            this.chbItemsHTML.AutoSize = true;
            this.chbItemsHTML.Checked = true;
            this.chbItemsHTML.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbItemsHTML.Location = new System.Drawing.Point(24, 30);
            this.chbItemsHTML.Name = "chbItemsHTML";
            this.chbItemsHTML.Size = new System.Drawing.Size(91, 17);
            this.chbItemsHTML.TabIndex = 3;
            this.chbItemsHTML.Text = "Po položkách";
            this.chbItemsHTML.UseVisualStyleBackColor = true;
            // 
            // chbProjectsHTML
            // 
            this.chbProjectsHTML.AutoSize = true;
            this.chbProjectsHTML.Checked = true;
            this.chbProjectsHTML.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbProjectsHTML.Location = new System.Drawing.Point(24, 53);
            this.chbProjectsHTML.Name = "chbProjectsHTML";
            this.chbProjectsHTML.Size = new System.Drawing.Size(92, 17);
            this.chbProjectsHTML.TabIndex = 4;
            this.chbProjectsHTML.Text = "Po projektech";
            this.chbProjectsHTML.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chbProjectsCSV);
            this.groupBox2.Controls.Add(this.chbItemsCSV);
            this.groupBox2.Location = new System.Drawing.Point(235, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 89);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Excel";
            // 
            // chbProjectsCSV
            // 
            this.chbProjectsCSV.AutoSize = true;
            this.chbProjectsCSV.Checked = true;
            this.chbProjectsCSV.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbProjectsCSV.Location = new System.Drawing.Point(24, 53);
            this.chbProjectsCSV.Name = "chbProjectsCSV";
            this.chbProjectsCSV.Size = new System.Drawing.Size(92, 17);
            this.chbProjectsCSV.TabIndex = 6;
            this.chbProjectsCSV.Text = "Po projektech";
            this.chbProjectsCSV.UseVisualStyleBackColor = true;
            // 
            // chbItemsCSV
            // 
            this.chbItemsCSV.AutoSize = true;
            this.chbItemsCSV.Checked = true;
            this.chbItemsCSV.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbItemsCSV.Location = new System.Drawing.Point(24, 30);
            this.chbItemsCSV.Name = "chbItemsCSV";
            this.chbItemsCSV.Size = new System.Drawing.Size(91, 17);
            this.chbItemsCSV.TabIndex = 5;
            this.chbItemsCSV.Text = "Po položkách";
            this.chbItemsCSV.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Location = new System.Drawing.Point(24, 79);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(445, 119);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Výběr exportu:";
            // 
            // btOK
            // 
            this.btOK.Enabled = false;
            this.btOK.Location = new System.Drawing.Point(394, 216);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 7;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(313, 216);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 8;
            this.btCancel.Text = "Zrušit";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // tbFile
            // 
            this.tbFile.Location = new System.Drawing.Point(102, 53);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(287, 20);
            this.tbFile.TabIndex = 2;
            this.tbFile.TextChanged += new System.EventHandler(this.tbFolder_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Název:";
            // 
            // PickExportDialog
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(495, 257);
            this.Controls.Add(this.tbFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tbFolder);
            this.Controls.Add(this.btFind);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PickExportDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btFind;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbProjectsHTML;
        private System.Windows.Forms.CheckBox chbItemsHTML;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chbProjectsCSV;
        private System.Windows.Forms.CheckBox chbItemsCSV;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.Label label2;
    }
}