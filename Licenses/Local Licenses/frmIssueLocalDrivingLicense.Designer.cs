namespace DVLD.Licenses.Local_Licenses
{
    partial class frmIssueLocalDrivingLicense
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
            this.ctrlLocalDrivingLicenseApplicationInfo1 = new DVLD.Applications.Local_Driving_License_Application.ctrlLocalDrivingLicenseApplicationInfo();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnIssue = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlLocalDrivingLicenseApplicationInfo1
            // 
            this.ctrlLocalDrivingLicenseApplicationInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlLocalDrivingLicenseApplicationInfo1.Location = new System.Drawing.Point(13, 14);
            this.ctrlLocalDrivingLicenseApplicationInfo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlLocalDrivingLicenseApplicationInfo1.Name = "ctrlLocalDrivingLicenseApplicationInfo1";
            this.ctrlLocalDrivingLicenseApplicationInfo1.Size = new System.Drawing.Size(945, 357);
            this.ctrlLocalDrivingLicenseApplicationInfo1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 368);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Notes:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Notes_32;
            this.pictureBox1.Location = new System.Drawing.Point(90, 362);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(140, 365);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(807, 170);
            this.txtNotes.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(695, 541);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(123, 43);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnIssue
            // 
            this.btnIssue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIssue.Image = global::DVLD.Properties.Resources.IssueDrivingLicense_32;
            this.btnIssue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssue.Location = new System.Drawing.Point(824, 541);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(123, 43);
            this.btnIssue.TabIndex = 19;
            this.btnIssue.Text = "Issue";
            this.btnIssue.UseVisualStyleBackColor = true;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // frmIssueLocalDrivingLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 593);
            this.Controls.Add(this.btnIssue);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlLocalDrivingLicenseApplicationInfo1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmIssueLocalDrivingLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Issue Local Driving License";
            this.Load += new System.EventHandler(this.frmIssueLocalDrivingLicense_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Applications.Local_Driving_License_Application.ctrlLocalDrivingLicenseApplicationInfo ctrlLocalDrivingLicenseApplicationInfo1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnIssue;
    }
}