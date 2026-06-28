using DVLD.Global;
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

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {
        private enum enMode { AddNew, Update}
        private enMode _Mode;

        private int _TestAppointmentID;
        private clsTest _Test;
        private clsTestType.enTestTypes _TestTypeID;

        public frmTakeTest(int TestAppointmentID,clsTestType.enTestTypes TestTypeID)
        {
            InitializeComponent();

            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlTakeTest1.TestTypeID = _TestTypeID;
            ctrlTakeTest1.LoadTestInfo(_TestAppointmentID);

            if (ctrlTakeTest1.TestID == -1)
            {
                _Mode = enMode.AddNew;
                rbPass.Checked = true;
                _Test = new clsTest();
            }
            else
            {
                _Mode = enMode.Update;
                _Test = clsTest.GetTestInfoByID(ctrlTakeTest1.TestID);

                if (_Test == null)
                {
                    MessageBox.Show($"No Test with ID = {ctrlTakeTest1.TestID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSave.Enabled = false;
                    return;
                }

                txtNotes.Text = _Test.Notes;
                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;

                lbCannotChange.Visible = true;
                rbPass.Enabled = false;
                rbFail.Enabled = false;
            }

           


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                _Test.TestAppointmentID = _TestAppointmentID;
                _Test.TestResult = (rbPass.Checked) ? true : false;
                _Test.Notes = txtNotes.Text;
                _Test.CreatedByUserID = clsGlobal.User.UserID;
            }
            else
                _Test.Notes = txtNotes.Text;

            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Confirm!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update;
                ctrlTakeTest1.TestID = _Test.TestID;
                btnSave.Enabled = false;
            }
            else
                MessageBox.Show("Data did not saved", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
