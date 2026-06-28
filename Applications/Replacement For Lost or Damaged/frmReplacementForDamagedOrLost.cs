using DVLD.Global;
using DVLD_Bussiness;
using DVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using DVLD.Drivers;

namespace DVLD.Applications.Replacement_For_Lost_or_Damaged
{
    public partial class frmReplacementForDamagedOrLost : Form
    {

        private clsApplicationType _ApplicationType = clsApplicationType.GetApplicationTypeInfoByID(clsApplicationType.enApplicationType.ReplacementForDamage);
        private clsLicense _License;

        public frmReplacementForDamagedOrLost()
        {
            InitializeComponent();
        }

        private clsApplicationType.enApplicationType _GetApplicationTypeID()
        {
            if (rbDamaged.Checked)
                return clsApplicationType.enApplicationType.ReplacementForDamage;
            else
                return clsApplicationType.enApplicationType.ReplacementForLost;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenseCardWithFilter1_OnLicenseSelected(int LicenseID)
        {
            this._License = clsLicense.GetLicenseInfoByID(LicenseID);

            if (_License != null)
            {
                if(!_License.IsActive)
                {
                    MessageBox.Show("Selected license isn't active, Choose an active license", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnIssueReplacement.Enabled = false;
                    return;
                }

                lbOldLicenseID.Text = LicenseID.ToString();
                btnIssueReplacement.Enabled = true;
                lkbShowLicenseHistory.Enabled = true;
            }
            else
                MessageBox.Show("License was not Found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void rbDamaged_CheckedChanged(object sender, EventArgs e)
        {
            if(rbDamaged.Checked)
            {
                this.Text = "Replacement For Damaged";
                lbScreenHeader.Text = "Replacement For Damaged";
                _ApplicationType = clsApplicationType.GetApplicationTypeInfoByID(_GetApplicationTypeID());
            }
        }

        private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
            if(rbLost.Checked)
            {
                this.Text = "Replacement For Lost";
                lbScreenHeader.Text = "Replacement For Lost";
                _ApplicationType = clsApplicationType.GetApplicationTypeInfoByID(_GetApplicationTypeID());
            }
        }

        private void frmReplacementForDamagedOrLost_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseCardWithFilter1.FilterFoucs();

            lbApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lbApplicationFees.Text = _ApplicationType.ApplicationFees.ToString();
            lbCreatedBy.Text = clsGlobal.User.UserID.ToString();
            rbDamaged.Checked = true;
            
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            clsApplication ReplacementApplication = new clsApplication();
            clsLicense NewLicense = new clsLicense();

            ReplacementApplication.ApplicantPersonID = this._License.DriverInfo.PersonID;
            ReplacementApplication.ApplicationDate = DateTime.Now;
            ReplacementApplication.ApplicationTypeID = this._ApplicationType.ApplicationTypeID;
            ReplacementApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            ReplacementApplication.LastStatusDate = DateTime.Now;
            ReplacementApplication.PaidFees = this._ApplicationType.ApplicationFees;
            ReplacementApplication.CreatedByUserID = clsGlobal.User.UserID;
            ReplacementApplication.Save();


            NewLicense.ApplicationID = ReplacementApplication.ApplicationID;
            NewLicense.DriverID = this._License.DriverID;
            NewLicense.LicenseClass = this._License.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this._License.LicenseClassInfo.DefaultValidityLength);
            NewLicense.Notes = "";
            NewLicense.PaidFees = 0;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = (rbDamaged.Checked) ? clsLicense.enIssueReason.ReplacementForDamaged : clsLicense.enIssueReason.ReplacementForLost;
            NewLicense.CreatedByUserID = clsGlobal.User.UserID;
            this._License.IsActive = false;

            if(MessageBox.Show("Are you sure you want to issue this license?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (NewLicense.Save() && this._License.Save())
                {
                    MessageBox.Show($"License Replaced Successfully with ID = {NewLicense.LicenseID}", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnIssueReplacement.Enabled = false;
                    lkbShowLicenseInfo.Enabled = true;
                    ctrlDriverLicenseCardWithFilter1.FilterEnabled = false;
                    lbLicenseReplacementApplicationID.Text = ReplacementApplication.ApplicationID.ToString();
                    lbReplacedLicenseID.Text = NewLicense.LicenseID.ToString();
                }
                else
                    MessageBox.Show("Data did not saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkbShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmDriverLicenseHistory(this._License.DriverID);
            frm.ShowDialog();
        }

        private void lkbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LicenseID = Convert.ToInt32(lbReplacedLicenseID.Text);

            Form frm = new frmDriverLicenseInfo(LicenseID);
            frm.ShowDialog();
               
        }
    }
}
