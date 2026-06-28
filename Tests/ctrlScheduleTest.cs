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
    public partial class ctrlScheduleTest : UserControl
    {
        private enum enMode { AddNew = 0, Update = 1}
        private enMode _Mode;

        private enum enCreationMode { FirstTime = 0, Retake = 1};
        private enCreationMode _CreationMode;

        private int _TestAppointmentID;
        private int _LocalDrivingLicenseApplicationID;
        private clsTestAppointment _Appointment;
        private clsLocalDrivingLicenseApplication _LDLApp;

        private clsTestType.enTestTypes _TestTypeID;
        private clsTestType _TestType;
        public clsTestType.enTestTypes TestTypeID 
        { 
            get 
            { 
                return _TestTypeID; 
            } 
            set
            {
                _TestTypeID = value;
                switch(_TestTypeID)
                {
                    case clsTestType.enTestTypes.Vision:
                        pbScreenImage.Image = Resources.Vision_512;
                        gbAppointmentInfo.Text = "Vision Test";
                        break;
                    case clsTestType.enTestTypes.Written:
                        pbScreenImage.Image = Resources.Written_Test_512;
                        gbAppointmentInfo.Text = "Written Test";
                        break;
                    case clsTestType.enTestTypes.Practical:
                        pbScreenImage.Image = Resources.driving_test_512;
                        gbAppointmentInfo.Text = "Practical Test";
                        break;
                }
            }
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID, int AppointmentID = -1)
        {
            if (AppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;
            _LDLApp = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID);

            if(_LDLApp == null)
            {
                MessageBox.Show($"No Application with ID = {LocalDrivingLicenseApplicationID.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //incase the LDLApp was found
            lbDrivingLicenseAppID.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();
            lbDrivingClass.Text = _LDLApp.LicenseClassInfo.ClassName;
            lbName.Text = _LDLApp.PersonInfo.FullName.ToString();
            lbTrial.Text = clsLocalDrivingLicenseApplication.GetTestTrialsPerTestType(_LDLApp.LocalDrivingLicenseApplicationID, _TestTypeID).ToString();
            _TestType = clsTestType.GetTestTypeInfoByID(_TestTypeID);
            dtpDate.Value = DateTime.Now;
            lbFees.Text = _TestType.TestTypeFees.ToString();

           

            if (_LDLApp.DoesAttnedTestType(_TestTypeID))
                _CreationMode = enCreationMode.Retake;
            else
                _CreationMode = enCreationMode.FirstTime;


            if (_CreationMode == enCreationMode.Retake)
            {
                float ApplicationFees = clsApplicationType.GetApplicationTypeInfoByID(clsApplicationType.enApplicationType.RetakeTest).ApplicationFees;
                lbRetakeApplicationFees.Text = ApplicationFees.ToString();
                lbTotalFees.Text = (ApplicationFees + _TestType.TestTypeFees).ToString();
                gbRetakeAppInfo.Enabled = true;
                lbTitle.Text = "Sehedule Retake Test";
            }
            else
            {
                lbTotalFees.Text = _TestType.TestTypeFees.ToString();
                gbRetakeAppInfo.Enabled = false;
                lbTitle.Text = "Sehedule Test";
            }

            if (_Mode == enMode.AddNew)
            {
                dtpDate.MinDate = DateTime.Now;
                dtpDate.Value = dtpDate.MinDate;

                _Appointment = new clsTestAppointment();
            }
            else
            {
                if (!_LoadAppointmentData())
                    return;
            }

            if (!_HandleLockedAppointmentCase())
                return;

            if (!_HandleActiveAppointment())
                return;

            if (!_HandlePreviousTest())
                return;
        }

        private bool _LoadAppointmentData()
        {
            _Appointment = clsTestAppointment.GetTestAppointmentInfoByID(_TestAppointmentID);

            if(_Appointment == null)
            {
                MessageBox.Show($"No Appointment with ID {_TestAppointmentID.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;
            }

            //in case appointment was found
            if (DateTime.Compare(DateTime.Now, _Appointment.AppointmentDate) < 0)
                dtpDate.MinDate = DateTime.Now;
            else
                dtpDate.MinDate = _Appointment.AppointmentDate;

            dtpDate.Value = _Appointment.AppointmentDate;

            if (_Appointment.RetakeApplicationInfo != null)
            {
                lbRetakeApplicationFees.Text = _Appointment.RetakeApplicationInfo.PaidFees.ToString();
                lbRetakeApplicationID.Text = _Appointment.RetakeApplicationID.ToString();
                gbRetakeAppInfo.Enabled = true;
                lbTitle.Text = "Sechdule Retake Test";
            }

            return true;

            
        }
        private bool _HandleActiveAppointment()
        {
            if(_Mode == enMode.AddNew && clsTestAppointment.DoesPersonHaveAnActiveAppointment(_LocalDrivingLicenseApplicationID,_TestTypeID))
            {
                lbFaild.Text = "Person alreay has an active appointment";
                lbFaild.Visible = true;
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;
            }

            return true;
        }
        private bool _HandleLockedAppointmentCase()
        {
            if (_Appointment != null)
                if (_Mode == enMode.Update && _Appointment.IsLocked)
                {
                    lbFaild.Text = "Person already sat for this test,appointment is locked";
                    lbFaild.Visible = true;
                    btnSave.Enabled = false;
                    dtpDate.Enabled = false;
                    return false;
                }

            return true;
        }
        private bool _HandlePreviousTest()
        {
            switch(_TestTypeID)
            {
                case clsTestType.enTestTypes.Vision:
                    lbFaild.Visible = false;
                    return true;
                case clsTestType.enTestTypes.Written:
                    if (!_LDLApp.DoesPassTestType(clsTestType.enTestTypes.Vision))
                    {
                        lbFaild.Text = "Not Allowed person has to take Vision Test before";
                        lbFaild.Visible = true;
                        btnSave.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lbFaild.Visible = false;
                        btnSave.Visible = true;
                        return true;
                    }
                case clsTestType.enTestTypes.Practical:
                    if (!_LDLApp.DoesPassTestType(clsTestType.enTestTypes.Written))
                    {
                        lbFaild.Text = "Not Allowed person has to take Written Test before";
                        lbFaild.Visible = true;
                        btnSave.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lbFaild.Visible = false;
                        btnSave.Visible = true;
                        return true;
                    }
            }

            return true;
        }
        private void _HandleRetakeTestApplication()
        {
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.Retake)
            {
                clsApplication application = new clsApplication();
                application.ApplicantPersonID = _LDLApp.ApplicantPersonID;
                application.ApplicationDate = DateTime.Now;
                application.ApplicationTypeID = clsApplicationType.enApplicationType.RetakeTest;
                application.ApplicationStatus = clsApplication.enApplicationStatus.New;
                application.LastStatusDate = DateTime.Now;
                application.PaidFees = clsApplicationType.GetApplicationTypeInfoByID(clsApplicationType.enApplicationType.RetakeTest).ApplicationFees;
                application.CreatedByUserID = clsGlobal.User.UserID;
                application.Save();
                lbRetakeApplicationID.Text = application.ApplicationID.ToString();

                _Appointment.RetakeApplicationID = application.ApplicationID;
            }
            else
                _Appointment.RetakeApplicationID = null;
        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _HandleRetakeTestApplication();

            _Appointment.TestTypeID = _TestTypeID;
            _Appointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _Appointment.AppointmentDate = dtpDate.Value;
            _Appointment.PaidFees = _TestType.TestTypeFees;
            _Appointment.CreatedByUserID = clsGlobal.User.UserID;
            _Appointment.IsLocked = false;

            if (_Appointment.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Confirm!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update;
            }
            else
                MessageBox.Show("Data did not saved", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
