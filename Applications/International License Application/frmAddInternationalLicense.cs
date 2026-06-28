using DVLD.Global;
using DVLD_Bussiness;
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

namespace DVLD.Licenses.International_Licenses
{
    public partial class frmAddInternationalLicense : Form
    {
        private clsApplicationType _ApplicationType = clsApplicationType.GetApplicationTypeInfoByID(clsApplicationType.enApplicationType.NewInternationalLicense);
        private clsLicense _License;

        public frmAddInternationalLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddInternationalLicense_Load(object sender, EventArgs e)
        {
            lbApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lbIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lbFees.Text = _ApplicationType.ApplicationFees.ToString();
            lbEpxirationDate.Text = DateTime.Now.AddYears(clsInternationalLicense.GetInternationalLicenseDefaultValidityLength()).ToString("dd/MMM/yyyy");
            lbCreatedBy.Text = clsGlobal.User.UserName;
        }

        private void ctrlDriverLicenseCardWithFilter1_OnLicenseSelected(int LicenseID)
        {
            _License = clsLicense.GetLicenseInfoByID(LicenseID);

            if(_License == null)
            {
                MessageBox.Show("Selected License with ID = " + LicenseID.ToString() + " was not found", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            if(clsInternationalLicense.DoesDriverHaveActiveInternationalLicense(_License.DriverID))
            {
                MessageBox.Show("Person already has an active international license,\nchoose another one.", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            if(!_License.IsActive)
            {
                MessageBox.Show("Selected license isn't active, please select active one", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }    

            if(DateTime.Now > _License.ExpirationDate)
            {
                MessageBox.Show("Selected license isn't valid due to it is Expired","Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

           if(_License.LicenseClass != clsLicenseClass.enLicenseClasses.Ordinary)
            {
                MessageBox.Show("Issue international license is allowed for ordinary license driving class only!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            lbLocalLicenseID.Text = LicenseID.ToString();
            btnIssue.Enabled = true;
            lkbShowLicenseHistory.Enabled = true;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if(clsInternationalLicense.DoesDriverHaveActiveInternationalLicense(_License.DriverID))
            {
                MessageBox.Show("Driver already has an International License!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            //Create new application
            clsApplication application = new clsApplication();
            application.ApplicantPersonID = _License.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = _ApplicationType.ApplicationTypeID;
            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = _ApplicationType.ApplicationFees;
            application.CreatedByUserID = clsGlobal.User.UserID;
            bool AddApplicationSucceded = application.Save();

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();
            InternationalLicense.ApplicationID = application.ApplicationID;
            InternationalLicense.DriverID = _License.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = _License.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(clsInternationalLicense.GetInternationalLicenseDefaultValidityLength());
            InternationalLicense.IsActive = true;
            InternationalLicense.CreatedByUserID = clsGlobal.User.UserID;

            if(MessageBox.Show("Are you sure you want to issue the license?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(InternationalLicense.Save() && AddApplicationSucceded)
                {
                    MessageBox.Show($"International license issued successfully with ID = {InternationalLicense.InternationalLicenseID}", "Confirm!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbInternationalApplicationID.Text = application.ApplicationID.ToString();
                    lbInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
                    btnIssue.Enabled = false;
                    lkbShowLicenseInfo.Enabled = true;
                    ctrlDriverLicenseCardWithFilter1.FilterEnabled = false;
                }
                else
                    MessageBox.Show("Data did not saved!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkbShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmDriverLicenseHistory(_License.DriverID);
            frm.ShowDialog();
        }

        private void lkbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmInternationalLicenseInfo(Convert.ToInt32(lbInternationalLicenseID.Text));
            frm.ShowDialog();
        }

        private void frmAddInternationalLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseCardWithFilter1.FilterFoucs();
        }
    }
}
