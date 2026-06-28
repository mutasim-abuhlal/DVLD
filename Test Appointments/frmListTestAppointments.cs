using DVLD.Properties;
using DVLD_Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests.Test_Appointments
{
    public partial class frmListTestAppointments : Form
    {
        private DataTable _dtTestAppointments;
        private clsTestType.enTestTypes _TestTypeID;
        private clsLocalDrivingLicenseApplication _LDLApp;

        public frmListTestAppointments(int LocalDrivingLicenseApplicationID,clsTestType.enTestTypes TestTypeID)
        {
            InitializeComponent();

            _TestTypeID = TestTypeID;
            _LDLApp = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID);
        }

        private void _SetFormView()
        {
            switch(_TestTypeID)
            {
                case clsTestType.enTestTypes.Vision:
                    pictureBox1.Image = Resources.Vision_512;
                    lbScreenHeader.Text = "Vision Test Appointemnts";
                    break;
                case clsTestType.enTestTypes.Written:
                    pictureBox1.Image = Resources.Written_Test_512;
                    lbScreenHeader.Text = "Written Test Appointments";
                    break;
                case clsTestType.enTestTypes.Practical:
                    pictureBox1.Image = Resources.driving_test_512;
                    lbScreenHeader.Text = "Street Test Appointments";
                    break;
            }

            this.Text = lbScreenHeader.Text;
        }

        private void _RefreshTestAppointments()
        {
            _dtTestAppointments = clsTestAppointment.GetAllPersonTestAppointmentsForGivenTestType(_LDLApp.LocalDrivingLicenseApplicationID, _TestTypeID);
            dgvTestAppointments.DataSource = _dtTestAppointments;

            lbTotalRecords.Text = dgvTestAppointments.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _SetFormView();
            _RefreshTestAppointments();

            if(dgvTestAppointments.Rows.Count > 0)
            {
                dgvTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvTestAppointments.Columns[0].Width = 190;

                dgvTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvTestAppointments.Columns[1].Width = 220;

                dgvTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvTestAppointments.Columns[2].Width = 140;

                dgvTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvTestAppointments.Columns[3].Width = 100;
            }

            if (_LDLApp != null)
                ctrlLocalDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationInfo(_LDLApp.LocalDrivingLicenseApplicationID);
        }

        private void btnSchduelTest_Click(object sender, EventArgs e)
        {
            if(clsTestAppointment.DoesPersonHaveAnActiveAppointment(_LDLApp.LocalDrivingLicenseApplicationID,_TestTypeID))
            {
                MessageBox.Show("Person already has an active appointment\n you cannot add one more!", "Has Appointment!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsTest.DoesPersonPassedTest(_LDLApp.LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                MessageBox.Show("Person already Passed this test before,you can only\nretake failed test", "Not Allowed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form frm = new frmScheduelTest(_TestTypeID, _LDLApp.LocalDrivingLicenseApplicationID);
            frm.ShowDialog();

            frmListTestAppointments_Load(null, null);
            ctrlLocalDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationInfo(_LDLApp.LocalDrivingLicenseApplicationID);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmScheduelTest(_TestTypeID, _LDLApp.LocalDrivingLicenseApplicationID, (int)dgvTestAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshTestAppointments();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmTakeTest((int)dgvTestAppointments.CurrentRow.Cells[0].Value,_TestTypeID);
            frm.ShowDialog();

            _RefreshTestAppointments();
        }
    }
}
