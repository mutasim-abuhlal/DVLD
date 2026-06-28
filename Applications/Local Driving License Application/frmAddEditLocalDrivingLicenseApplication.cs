using DVLD.Global;
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

namespace DVLD.Applications.Local_Driving_License_Application
{
    public partial class frmAddEditLocalDrivingLicenseApplication : Form
    {
        private enum enMode { AddNew,Update}
        private enMode _Mode;
        private clsLocalDrivingLicenseApplication _LDLApp;
        private clsApplicationType _ApplicationType = clsApplicationType.GetApplicationTypeInfoByID(clsApplicationType.enApplicationType.NewLicense);

        private int _PersonID;
        public frmAddEditLocalDrivingLicenseApplication()
        {
            InitializeComponent();

            _PersonID = -1;
            _Mode = enMode.AddNew;
            _LDLApp = new clsLocalDrivingLicenseApplication();
        }

        public frmAddEditLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();

            _LDLApp = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID);
            if( _LDLApp != null )
            {
                _PersonID = _LDLApp.ApplicantPersonID;
            }
            _Mode = enMode.Update;
        }

        private void _FillLicenseClassesInComboBox()
        {
            foreach(DataRow row in clsLicenseClass.GetAllLicenseClasses().Rows)
            {
                cbLicenseClasses.Items.Add(row["ClassName"]);
            }
        }

        private void _ResetDefaultValues()
        {
            lbDLApplicationID.Text = "[???]";
            lbApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            cbLicenseClasses.SelectedIndex = cbLicenseClasses.FindString("Class 3 - Ordinary driving license");
            lbApplicationFees.Text = _ApplicationType.ApplicationFees.ToString();
            lbCreatedBy.Text = clsGlobal.User.UserName;
        }

        private void _LoadData()
        {
            this.Text = lbScreenHeader.Text;
            lbScreenHeader.Text = "Update Local Driving License Application";

            lbDLApplicationID.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();
            lbApplicationDate.Text = _LDLApp.ApplicationDate.ToString("dd/MMM/yyyy");
            cbLicenseClasses.SelectedIndex = cbLicenseClasses.FindString(_LDLApp.LicenseClassInfo.ClassName);
            lbApplicationFees.Text = _LDLApp.PaidFees.ToString();
            lbCreatedBy.Text = _LDLApp.UserInfo.UserName;

            ctrlPersonInfoWithFilter1.LoadPersonInfo(_PersonID);
            ctrlPersonInfoWithFilter1.FilterEnabled = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(this._Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tabControl1.SelectedTab = tabControl1.TabPages["tpApplicationInfo"];
            }

            if(_PersonID == -1)
            {
                MessageBox.Show("Select a Person!", "Not Selected!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnSave.Enabled = (_PersonID != -1);
                tabControl1.SelectedTab = tabControl1.TabPages["tpApplicationInfo"];
            }
        }

        private void ctrlPersonInfoWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;
        }

        private void frmAddEditLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillLicenseClassesInComboBox();
            _ResetDefaultValues();

            if(_Mode == enMode.Update)
            {
                if (_LDLApp != null)
                    _LoadData();
            }

            this.ActiveControl = cbLicenseClasses;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("there are some empty fields you have not filled. Please check out them.", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }

            int ApplicationID = -1;
            if((ApplicationID = clsLocalDrivingLicenseApplication.DoesPersonHaveAnActiveApplicationWithSameLicenseClass(_PersonID, (int)clsLicenseClass.GetLicenseClassInfoByClassName(cbLicenseClasses.Text).LicenseClassID)) > -1)
            {
                MessageBox.Show($"Choose another License Class, the selected Person already\nhas an active application for the Selected With ID = {ApplicationID}","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if(clsLicense.DoesPersonHaveLicenseWithTheSameAppliedLicenseClass(_PersonID, clsLicenseClass.GetLicenseClassInfoByClassName(cbLicenseClasses.Text).LicenseClassID))
            {
                MessageBox.Show("Person already has a license with the same applied driving\nclass, Choose diffrent driving class.", "Not Allowed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LDLApp.ApplicantPersonID = _PersonID;
            _LDLApp.ApplicationDate = DateTime.Now;
            _LDLApp.ApplicationTypeID = _ApplicationType.ApplicationTypeID;
            _LDLApp.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LDLApp.LastStatusDate = DateTime.Now;
            _LDLApp.PaidFees = _ApplicationType.ApplicationFees;
            _LDLApp.CreatedByUserID = clsGlobal.User.UserID;

            _LDLApp.LicenseClassID = clsLicenseClass.GetLicenseClassInfoByClassName(cbLicenseClasses.Text).LicenseClassID;

            if(_LDLApp.Save())
            {
                MessageBox.Show("Data Saved Successfully!", "Confirm!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (_Mode == enMode.AddNew)
                {
                    lbScreenHeader.Text = "Update Local Driving License Application";
                    this.Text = lbScreenHeader.Text;
                    lbDLApplicationID.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();
                }
            }
            else
                MessageBox.Show("Data did not saved!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
