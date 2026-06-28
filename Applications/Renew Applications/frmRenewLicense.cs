using DVLD.Global;
using DVLD_Bussiness;
using DVLD.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Drivers;

namespace DVLD.Applications.Renew_Applications
{
    public partial class frmRenewLicense : Form
    {
        private clsLicense _License;
        private clsApplicationType _ApplicationType = clsApplicationType.GetApplicationTypeInfoByID(clsApplicationType.enApplicationType.RenewLicense);

        public frmRenewLicense()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            ctrlDriverLicenseCardWithFilter1.FilterFoucs();

            lbApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lbIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lbApplicationFees.Text = _ApplicationType.ApplicationFees.ToString();
            lbCreatedBy.Text = clsGlobal.User.UserName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRenewLicense_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
        }

        private void ctrlDriverLicenseCardWithFilter1_OnLicenseSelected(int LicenseID)
        {
            _License = clsLicense.GetLicenseInfoByID(LicenseID);

            if (_License != null)
            {
                lbOldLicenseID.Text = LicenseID.ToString();
                lbLicenseFees.Text = _License.LicenseClassInfo.ClassFees.ToString();
                lbTotalFees.Text = (_License.LicenseClassInfo.ClassFees + _ApplicationType.ApplicationFees).ToString();
                txtNotes.Text = _License.Notes;

                btnRenew.Enabled = true;
                lkbShowLicenseHistory.Enabled = true;

                if(_License.ExpirationDate > DateTime.Now)
                {
                    MessageBox.Show($"Selected NewLicense isn't expired yet, it will expire on\n{_License.ExpirationDate}", "Not Allowed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnRenew.Enabled = false;
                    return;
                }

                if (!_License.IsActive)
                {
                    MessageBox.Show("Selected NewLicense isn't Active, choose an active NewLicense", "Not Allowed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnRenew.Enabled = false;
                    return;
                }
            }
            else
                MessageBox.Show("License was not Found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            clsApplication application = new clsApplication();
            clsLicense NewLicense = new clsLicense();

            application.ApplicantPersonID = _License.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = this._ApplicationType.ApplicationTypeID;
            application.ApplicationStatus = clsApplication.enApplicationStatus.New;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = this._ApplicationType.ApplicationFees;
            application.CreatedByUserID = clsGlobal.User.UserID;

            if(!application.Save())
            {
                MessageBox.Show("Faild to Renew license", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }


            NewLicense.ApplicationID = application.ApplicationID;
            NewLicense.DriverID = this._License.DriverID;
            NewLicense.LicenseClass = this._License.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this._License.LicenseClassInfo.DefaultValidityLength);
            NewLicense.Notes = txtNotes.Text;
            NewLicense.PaidFees = this._License.LicenseClassInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = clsGlobal.User.UserID;
            this._License.IsActive = false;

            if (MessageBox.Show("Are you sure you want to renew the NewLicense?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (NewLicense.Save() && this._License.Save())
                {
                    MessageBox.Show($"License Renewed Successfully with ID = {NewLicense.LicenseID}", "Confirm!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbRenewedLicenseID.Text = NewLicense.LicenseID.ToString();
                    lbRenewLicenseApplication.Text = application.ApplicationID.ToString();
                    ctrlDriverLicenseCardWithFilter1.FilterEnabled = false;
                    lkbShowLicenseInfo.Enabled = true;
                    btnRenew.Enabled = false;
                }
                else
                    MessageBox.Show("Faild to Renew license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkbShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmDriverLicenseHistory(_License.DriverID);
            frm.ShowDialog();
        }

        private void lkbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int RenewedLicenseID = Convert.ToInt32(lbRenewedLicenseID.Text);

            Form frm = new frmDriverLicenseInfo(RenewedLicenseID);
            frm.ShowDialog();
        }
    }
}
