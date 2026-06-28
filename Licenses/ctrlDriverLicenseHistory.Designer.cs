namespace DVLD.Drivers
{
    partial class ctrlDriverLicenseHistory
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpLocal = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.lbLocalRecords = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvLocal = new System.Windows.Forms.DataGridView();
            this.tpInternational = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.lbInternationalRecords = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvInternational = new System.Windows.Forms.DataGridView();
            this.cmsLocal = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsInternational = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showLicenseInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInternationalLicenseInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocal)).BeginInit();
            this.tpInternational.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternational)).BeginInit();
            this.cmsLocal.SuspendLayout();
            this.cmsInternational.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1113, 341);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver Licenses";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpLocal);
            this.tabControl1.Controls.Add(this.tpInternational);
            this.tabControl1.Location = new System.Drawing.Point(6, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1101, 296);
            this.tabControl1.TabIndex = 0;
            // 
            // tpLocal
            // 
            this.tpLocal.BackColor = System.Drawing.SystemColors.Control;
            this.tpLocal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpLocal.Controls.Add(this.label4);
            this.tpLocal.Controls.Add(this.lbLocalRecords);
            this.tpLocal.Controls.Add(this.label3);
            this.tpLocal.Controls.Add(this.dgvLocal);
            this.tpLocal.Location = new System.Drawing.Point(4, 29);
            this.tpLocal.Name = "tpLocal";
            this.tpLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tpLocal.Size = new System.Drawing.Size(1093, 263);
            this.tpLocal.TabIndex = 0;
            this.tpLocal.Text = "Local";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Local Licenses History:";
            // 
            // lbLocalRecords
            // 
            this.lbLocalRecords.AutoSize = true;
            this.lbLocalRecords.Location = new System.Drawing.Point(110, 231);
            this.lbLocalRecords.Name = "lbLocalRecords";
            this.lbLocalRecords.Size = new System.Drawing.Size(44, 20);
            this.lbLocalRecords.TabIndex = 4;
            this.lbLocalRecords.Text = "[???]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "#Records:";
            // 
            // dgvLocal
            // 
            this.dgvLocal.AllowUserToAddRows = false;
            this.dgvLocal.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvLocal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocal.ContextMenuStrip = this.cmsLocal;
            this.dgvLocal.GridColor = System.Drawing.SystemColors.Control;
            this.dgvLocal.Location = new System.Drawing.Point(22, 37);
            this.dgvLocal.Name = "dgvLocal";
            this.dgvLocal.Size = new System.Drawing.Size(1051, 191);
            this.dgvLocal.TabIndex = 1;
            // 
            // tpInternational
            // 
            this.tpInternational.BackColor = System.Drawing.SystemColors.Control;
            this.tpInternational.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpInternational.Controls.Add(this.label2);
            this.tpInternational.Controls.Add(this.lbInternationalRecords);
            this.tpInternational.Controls.Add(this.label1);
            this.tpInternational.Controls.Add(this.dgvInternational);
            this.tpInternational.Location = new System.Drawing.Point(4, 29);
            this.tpInternational.Name = "tpInternational";
            this.tpInternational.Padding = new System.Windows.Forms.Padding(3);
            this.tpInternational.Size = new System.Drawing.Size(1093, 263);
            this.tpInternational.TabIndex = 1;
            this.tpInternational.Text = "Internatinoal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(253, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "International Licenses History:";
            // 
            // lbInternationalRecords
            // 
            this.lbInternationalRecords.AutoSize = true;
            this.lbInternationalRecords.Location = new System.Drawing.Point(110, 231);
            this.lbInternationalRecords.Name = "lbInternationalRecords";
            this.lbInternationalRecords.Size = new System.Drawing.Size(44, 20);
            this.lbInternationalRecords.TabIndex = 2;
            this.lbInternationalRecords.Text = "[???]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "#Records:";
            // 
            // dgvInternational
            // 
            this.dgvInternational.AllowUserToAddRows = false;
            this.dgvInternational.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvInternational.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternational.ContextMenuStrip = this.cmsInternational;
            this.dgvInternational.GridColor = System.Drawing.SystemColors.Control;
            this.dgvInternational.Location = new System.Drawing.Point(22, 37);
            this.dgvInternational.Name = "dgvInternational";
            this.dgvInternational.Size = new System.Drawing.Size(1051, 191);
            this.dgvInternational.TabIndex = 0;
            // 
            // cmsLocal
            // 
            this.cmsLocal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLicenseInfoToolStripMenuItem});
            this.cmsLocal.Name = "cmsLocal";
            this.cmsLocal.Size = new System.Drawing.Size(186, 42);
            // 
            // cmsInternational
            // 
            this.cmsInternational.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInternationalLicenseInfoToolStripMenuItem});
            this.cmsInternational.Name = "cmsLocal";
            this.cmsInternational.Size = new System.Drawing.Size(256, 64);
            // 
            // showLicenseInfoToolStripMenuItem
            // 
            this.showLicenseInfoToolStripMenuItem.Image = global::DVLD.Properties.Resources.License_View_32;
            this.showLicenseInfoToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showLicenseInfoToolStripMenuItem.Name = "showLicenseInfoToolStripMenuItem";
            this.showLicenseInfoToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.showLicenseInfoToolStripMenuItem.Text = "Show License Info";
            this.showLicenseInfoToolStripMenuItem.Click += new System.EventHandler(this.showLicenseInfoToolStripMenuItem_Click);
            // 
            // showInternationalLicenseInfoToolStripMenuItem
            // 
            this.showInternationalLicenseInfoToolStripMenuItem.Image = global::DVLD.Properties.Resources.International_32;
            this.showInternationalLicenseInfoToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showInternationalLicenseInfoToolStripMenuItem.Name = "showInternationalLicenseInfoToolStripMenuItem";
            this.showInternationalLicenseInfoToolStripMenuItem.Size = new System.Drawing.Size(255, 38);
            this.showInternationalLicenseInfoToolStripMenuItem.Text = "Show International License Info";
            this.showInternationalLicenseInfoToolStripMenuItem.Click += new System.EventHandler(this.showInternationalLicenseInfoToolStripMenuItem_Click);
            // 
            // ctrlDriverLicenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ctrlDriverLicenseHistory";
            this.Size = new System.Drawing.Size(1113, 341);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpLocal.ResumeLayout(false);
            this.tpLocal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocal)).EndInit();
            this.tpInternational.ResumeLayout(false);
            this.tpInternational.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternational)).EndInit();
            this.cmsLocal.ResumeLayout(false);
            this.cmsInternational.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpLocal;
        private System.Windows.Forms.TabPage tpInternational;
        private System.Windows.Forms.DataGridView dgvInternational;
        private System.Windows.Forms.Label lbInternationalRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvLocal;
        private System.Windows.Forms.Label lbLocalRecords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip cmsLocal;
        private System.Windows.Forms.ToolStripMenuItem showLicenseInfoToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsInternational;
        private System.Windows.Forms.ToolStripMenuItem showInternationalLicenseInfoToolStripMenuItem;
    }
}
