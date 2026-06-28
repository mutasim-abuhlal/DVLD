using DVLD_Bussiness;
using DVLD.Licenses.Local_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Drivers;

namespace DVLD.Applications.Local_Driving_License_Application
{
    public partial class ctrlLocalDrivingLicenseApplicationInfo : UserControl
    {
        private int _LicenseID;
        public ctrlLocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadLocalDrivingLicenseApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            clsLocalDrivingLicenseApplication LDLApp =
                clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID);

            if(LDLApp != null)
            {
                _LicenseID = clsLicense.GetActiveLicenseID(LDLApp.ApplicantPersonID, LDLApp.LicenseClassID);
                llShowLicenceInfo.Enabled =  (this._LicenseID != -1);

                lbDrivingLicenseAppID.Text = LDLApp.LocalDrivingLicenseApplicationID.ToString();
                lbLicenseClass.Text        = LDLApp.LicenseClassInfo.ClassName;
                lbPassedTests.Text         = LDLApp.GetPassedTestCount().ToString() + "/3";
                ctrlApplicationBasicInfo1.LoadApplicationInfo(LDLApp.ApplicationID);
            }
        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmDriverLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }
    }
}
