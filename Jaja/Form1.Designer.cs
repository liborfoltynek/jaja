namespace Jaja
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.btLoad = new System.Windows.Forms.Button();
            this.lvAppointments = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btExport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.chbShowProjectsOnly = new System.Windows.Forms.CheckBox();
            this.lvKalendare = new System.Windows.Forms.ListView();
            this.btReset = new System.Windows.Forms.Button();
            this.btKalendar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.monthCalendar2 = new System.Windows.Forms.MonthCalendar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btLoad
            // 
            this.btLoad.Location = new System.Drawing.Point(12, 524);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(75, 23);
            this.btLoad.TabIndex = 1;
            this.btLoad.Text = "Načíst";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // lvAppointments
            // 
            this.lvAppointments.CheckBoxes = true;
            this.lvAppointments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader8,
            this.columnHeader1,
            this.columnHeader2});
            this.lvAppointments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAppointments.Location = new System.Drawing.Point(0, 0);
            this.lvAppointments.Name = "lvAppointments";
            this.lvAppointments.Size = new System.Drawing.Size(906, 624);
            this.lvAppointments.TabIndex = 1;
            this.lvAppointments.UseCompatibleStateImageBehavior = false;
            this.lvAppointments.View = System.Windows.Forms.View.Details;
            this.lvAppointments.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Projekt";
            this.columnHeader6.Width = 300;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Délka";
            this.columnHeader8.Width = 150;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Datum";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Kalendář";
            this.columnHeader2.Width = 180;
            // 
            // btExport
            // 
            this.btExport.Enabled = false;
            this.btExport.Location = new System.Drawing.Point(93, 562);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(69, 23);
            this.btExport.TabIndex = 2;
            this.btExport.Text = "Export";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.chbShowProjectsOnly);
            this.panel1.Controls.Add(this.lvKalendare);
            this.panel1.Controls.Add(this.btReset);
            this.panel1.Controls.Add(this.btKalendar);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.monthCalendar1);
            this.panel1.Controls.Add(this.monthCalendar2);
            this.panel1.Controls.Add(this.btExport);
            this.panel1.Controls.Add(this.btLoad);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(906, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 646);
            this.panel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(14, 617);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "open log folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chbShowProjectsOnly
            // 
            this.chbShowProjectsOnly.AutoSize = true;
            this.chbShowProjectsOnly.Location = new System.Drawing.Point(21, 595);
            this.chbShowProjectsOnly.Name = "chbShowProjectsOnly";
            this.chbShowProjectsOnly.Size = new System.Drawing.Size(83, 17);
            this.chbShowProjectsOnly.TabIndex = 13;
            this.chbShowProjectsOnly.Text = "Jen projekty";
            this.chbShowProjectsOnly.UseVisualStyleBackColor = true;
            this.chbShowProjectsOnly.CheckedChanged += new System.EventHandler(this.chbShowProjectsOnly_CheckedChanged);
            // 
            // lvKalendare
            // 
            this.lvKalendare.CheckBoxes = true;
            this.lvKalendare.Location = new System.Drawing.Point(12, 410);
            this.lvKalendare.Name = "lvKalendare";
            this.lvKalendare.ShowItemToolTips = true;
            this.lvKalendare.Size = new System.Drawing.Size(151, 97);
            this.lvKalendare.TabIndex = 12;
            this.lvKalendare.UseCompatibleStateImageBehavior = false;
            this.lvKalendare.View = System.Windows.Forms.View.List;
            this.lvKalendare.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvKalendare_ItemChecked);
            // 
            // btReset
            // 
            this.btReset.Location = new System.Drawing.Point(14, 562);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(73, 23);
            this.btReset.TabIndex = 11;
            this.btReset.Text = "Reset";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // btKalendar
            // 
            this.btKalendar.Location = new System.Drawing.Point(93, 524);
            this.btKalendar.Name = "btKalendar";
            this.btKalendar.Size = new System.Drawing.Size(69, 23);
            this.btKalendar.TabIndex = 0;
            this.btKalendar.Text = "Kalendář";
            this.btKalendar.UseVisualStyleBackColor = true;
            this.btKalendar.Click += new System.EventHandler(this.brKalendar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(12, 23);
            this.monthCalendar1.MaxSelectionCount = 1;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 5;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // monthCalendar2
            // 
            this.monthCalendar2.Location = new System.Drawing.Point(12, 189);
            this.monthCalendar2.MaxSelectionCount = 1;
            this.monthCalendar2.Name = "monthCalendar2";
            this.monthCalendar2.TabIndex = 6;
            this.monthCalendar2.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 624);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(906, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 646);
            this.Controls.Add(this.lvAppointments);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Outlook Exporter v15 by Libor Foltýnek 2013-2018";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.ListView lvAppointments;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.MonthCalendar monthCalendar2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btKalendar;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView lvKalendare;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chbShowProjectsOnly;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button button1;
    }
}

