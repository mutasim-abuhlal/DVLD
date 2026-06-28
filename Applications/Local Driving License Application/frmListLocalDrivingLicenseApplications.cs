using DVLD_Bussiness;
using DVLD.Tests;
using DVLD.Licenses;
using DVLD.Drivers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Tests.Test_Appointments;
using DVLD.Licenses.Local_Licenses;

namespace DVLD.Applications.Local_Driving_License_Application
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        private DataTable _dtLocalDrivingLicenseApplications;

        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void _RefreshLocalDrivingLicenseApplications()
        {
            _dtLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenesApplications();
            dgvLocalDrivingLicenseApplicaitons.DataSource = _dtLocalDrivingLicenseApplications;

            lbTotalRecords.Text = dgvLocalDrivingLicenseApplicaitons.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Status");
            cbStatus.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text == "Status");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            if (cbStatus.Visible)
            {
                cbStatus.Focus();
                cbStatus.SelectedIndex = 0;
            }
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditLocalDrivingLicenseApplication();
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplications();
        }

        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _RefreshLocalDrivingLicenseApplications();

            if (dgvLocalDrivingLicenseApplicaitons.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplicaitons.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplicaitons.Columns[0].Width = 100;

                dgvLocalDrivingLicenseApplicaitons.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplicaitons.Columns[1].Width = 250;

                dgvLocalDrivingLicenseApplicaitons.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplicaitons.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplicaitons.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplicaitons.Columns[3].Width = 330;

                dgvLocalDrivingLicenseApplicaitons.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplicaitons.Columns[4].Width = 150;

                dgvLocalDrivingLicenseApplicaitons.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplicaitons.Columns[5].Width = 150;

                dgvLocalDrivingLicenseApplicaitons.Columns[6].HeaderText = "Status";
                dgvLocalDrivingLicenseApplicaitons.Columns[6].Width = 140;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lbTotalRecords.Text = dgvLocalDrivingLicenseApplicaitons.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lbTotalRecords.Text = dgvLocalDrivingLicenseApplicaitons.Rows.Count.ToString();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStatus.Text == "All")
                _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
            else
                _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("Status LIKE '{0}%'", cbStatus.Text);
            

            lbTotalRecords.Text = dgvLocalDrivingLicenseApplicaitons.Rows.Count.ToString();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditLocalDrivingLicenseApplication((int)dgvLocalDrivingLicenseApplicaitons.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplications();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LDLApp =
                clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID((int)dgvLocalDrivingLicenseApplicaitons.CurrentRow.Cells[0].Value);

            if (MessageBox.Show("Are you sure you want to Cancel this application?", "Confirm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(LDLApp.LocalDrivingLicenseApplicationID) && clsLocalDrivingLicenseApplication.DeleteApplication(LDLApp.ApplicationID))
                    MessageBox.Show("Application Deleted Successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this._RefreshLocalDrivingLicenseApplications();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LDLApp =
                clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID((int)dgvLocalDrivingLicenseApplicaitons.CurrentRow.Cells[0].Value);

            if (MessageBox.Show("Are you sure you want to Cancel this application?", "Confirm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (LDLApp.Cancel())
                    MessageBox.Show("Application Cancelled Successfully!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this._RefreshLocalDrivingLicenseApplications();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            string Status = dgvLocalDrivingLicenseApplicaitons.CurrentRow.Cells[6].Value.ToString();
            byte PassedTests = Convert.ToByte(dgvLocalDrivingLicenseApplicaitons.CurrentRow.Cells[5].Value);

            editApplicationToolStripMenuItem.Enabled = (Status == "New");
            deleteApplicationToolStripMenuItem.Enabled = (Status == "New");
            cancelApplicationToolStripMenuItem.Enabled = (Status == "New");
            sechduelTestToolStripMenuItem.Enabled = (PassedTests != 3);

            if(sechduelTestToolStripMenuItem.Enabled)
            {
                visionTestToolStripMenuItem.Enabled = (PassedTests == 0);
                writtenTestToolStripMenuItem.Enabled = (PassedTests == 1);
                practicalStreetTestToolStripMenuItem.Enabled = (PassedTests == 2);
            }

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (PassedTests == 3 && Status == "New");
            showLicenseToolStripMenuItem.Enabled = (Status == "Completed");
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplicaitons.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void visionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListTestAppointments((int)dgvLocalDrivingLicenseApplicaitons.CurrentRow.Cells[0].Value, clsTestType.enTestTypes.Vision);
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplications();
        }

        private void writtenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListTestAppointments((int)dgvLocalDrivingLicenseApplicaitons.CurrentRow.Cells[0].Value, clsTestType.enTestTypes.Written);
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplications();
        }

        private void practicalStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListTestAppointments((int)dgvLocalDrivingLicenseApplicaitons.CurrentRow.Cells[0].Value, clsTestType.enTestTypes.Practical);
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplications();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmIssueLocalDrivingLicense((int)dgvLocalDrivingLicenseApplicaitons.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplications();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LDLApp = 
                clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID((int)dgvLocalDrivingLicenseApplicaitons.CurrentRow.Cells[0].Value);

            if(LDLApp != null)
            {
                int LicenseID = clsLicense.GetActiveLicenseID(LDLApp.ApplicantPersonID, LDLApp.LicenseClassID);
             
                Form frm = new frmDriverLicenseInfo(LicenseID);
                frm.ShowDialog();
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID((int)dgvLocalDrivingLicenseApplicaitons.CurrentRow.Cells[0].Value).ApplicantPersonID;
            int DriverID = clsDriver.GetDriverInfoByPersonID(PersonID).DriverID;

            Form frm = new frmDriverLicenseHistory(DriverID);
            frm.ShowDialog();
        }
    }
}
