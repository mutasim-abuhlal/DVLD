using DVLD.Global;
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

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmIssueLocalDrivingLicense : Form
    {
        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _LDLApp;

        public frmIssueLocalDrivingLicense(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _LDLApp = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIssueLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            if(_LDLApp != null)
            {
                ctrlLocalDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationInfo(_LDLApp.LocalDrivingLicenseApplicationID);
            }
            else
            {
                MessageBox.Show($"No Appliaction with ID = {_LocalDrivingLicenseApplicationID}");
                btnIssue.Enabled = false;
                return;
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            clsLicense License = new clsLicense();
            clsDriver driver = new clsDriver();

            if(!clsDriver.IsDrivereExistByPersonID(_LDLApp.ApplicantPersonID))
            {
                driver.PersonID = _LDLApp.ApplicantPersonID;
                driver.CreatedByUserID = clsGlobal.User.UserID;
                driver.CreatedDate = DateTime.Now;
                driver.Save();
            }
            else
                driver = clsDriver.GetDriverInfoByPersonID(_LDLApp.ApplicantPersonID);

            License.ApplicationID = _LDLApp.ApplicationID;
            License.DriverID = driver.DriverID;
            License.LicenseClass = _LDLApp.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(_LDLApp.LicenseClassInfo.DefaultValidityLength);
            License.Notes = txtNotes.Text;
            License.PaidFees = _LDLApp.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = clsGlobal.User.UserID;


            if (License.Save())
            {
                MessageBox.Show($"License Issued Successfully with License ID  = {License.LicenseID}", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _LDLApp.SetCompleted();
            }
            else
                MessageBox.Show("License did not Issue", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
