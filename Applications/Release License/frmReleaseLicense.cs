using DVLD.Global;
using DVLD;
using DVLD_Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Drivers;

namespace DVLD.Applications.Release_License
{
    public partial class frmReleaseLicense : Form
    {
        private int _LicenseID = -1;
        private clsLicense _License;
        private clsDetainedLicense _Detain;
        private clsApplicationType _ApplicationType = clsApplicationType.GetApplicationTypeInfoByID(clsApplicationType.enApplicationType.ReleaseDetainedLicense);

        public frmReleaseLicense()
        {
            InitializeComponent();
        }

        public frmReleaseLicense(int DetainID)
        {
            InitializeComponent();

            this._Detain = clsDetainedLicense.GetDetainedLicenseInfoByID(DetainID);
            if (this._Detain != null)
            {
                this._LicenseID = this._Detain.LicenseID;
                this._License = this._Detain.LicenseInfo;
            }
        }

        private void _LoadData()
        {
            lbDetainID.Text = _Detain.DetainID.ToString();
            lbDeatinDate.Text = _Detain.DetainDate.ToString("dd/MMM/yyyy");
            lbApplicationFees.Text = this._ApplicationType.ApplicationFees.ToString();
            lbTotalFees.Text = (this._ApplicationType.ApplicationFees + _Detain.FineFees).ToString();
            lbLicenseID.Text = this._License.LicenseID.ToString();
            lbCreatedBy.Text = clsGlobal.User.UserID.ToString();
            lbFineFees.Text = _Detain.FineFees.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenseCardWithFilter1_OnLicenseSelected(int LicenseID)
        {
            this._License = clsLicense.GetLicenseInfoByID(LicenseID);

            if (this._License != null)
            {
                if (!clsDetainedLicense.IsLicenseDetainedByLicenseID(LicenseID))
                {
                    MessageBox.Show("Selected License isn't Detained, Choose aonther one", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnRelease.Enabled = false;
                    return;
                }

                _Detain = clsDetainedLicense.GetDetainedLicenseInfoByLicenseID(LicenseID);
                _LoadData();

                btnRelease.Enabled = true;
                lkbShowLicenseHistory.Enabled = true;
            }
            else
            {
                MessageBox.Show("Selected license was not Found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            clsApplication application = new clsApplication();
            application.ApplicantPersonID = this._License.ApplicationInfo.ApplicantPersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = this._ApplicationType.ApplicationTypeID;
            application.ApplicationStatus = clsApplication.enApplicationStatus.New;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = this._ApplicationType.ApplicationFees;
            application.CreatedByUserID = clsGlobal.User.UserID;
            application.Save();

            this._Detain.IsReleased = true;
            this._Detain.ReleaseDate = DateTime.Now;
            this._Detain.ReleaseApplicationID = application.ApplicationID;
            this._Detain.ReleasedByUserID = clsGlobal.User.UserID;

            if(MessageBox.Show("Are you sure you want to Release this detained license?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (this._Detain.Save())
                {
                    MessageBox.Show("Detained License Released Successfully!", "Confirm!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbApplicationID.Text = application.ApplicationID.ToString();
                    lkbShowLicenseInfo.Enabled = true;
                    btnRelease.Enabled = false;
                    ctrlDriverLicenseCardWithFilter1.FilterEnabled = false;

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
            Form frm = new frmDriverLicenseInfo(this._License.LicenseID);
            frm.ShowDialog();
        }

        private void frmReleaseLicense_Load(object sender, EventArgs e)
        {
            if(this._Detain != null)
            {
                btnRelease.Enabled = true;
                ctrlDriverLicenseCardWithFilter1.FilterEnabled = false;
                ctrlDriverLicenseCardWithFilter1.LoadInfo(_License.LicenseID);
                lkbShowLicenseHistory.Enabled = true;
                _LoadData();
            }
            else
            {
                btnRelease.Enabled = false;
            }
                lbDeatinDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                lbApplicationFees.Text = this._ApplicationType.ApplicationFees.ToString();
                lbCreatedBy.Text = clsGlobal.User.UserName;
        }

        private void frmReleaseLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseCardWithFilter1.FilterFoucs();
        }
    }
}
