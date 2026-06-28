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
    public partial class ctrlTakeTest : UserControl
    {
        private int _TestAppointmentID;
        private int _TestID;
        private clsTestAppointment _TestAppointment;
        private clsTest _Test;

        private clsTestType.enTestTypes _TestTypeID = clsTestType.enTestTypes.Vision;
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
                        pbImgeHeader.Image = Resources.Vision_512;
                        groupBox1.Text = "Vision Test";
                        break;
                    case clsTestType.enTestTypes.Written:
                        pbImgeHeader.Image = Resources.Written_Test_512;
                        groupBox1.Text = "Written Test";
                        break;
                    case clsTestType.enTestTypes.Practical:
                        pbImgeHeader.Image = Resources.driving_test_512;
                        groupBox1.Text = "Practical Test";
                        break;
                }
            }
        }

        public int TestID { get { return _TestID; } 
            set
            { 
                _TestID = value;
                lbTestID.Text = _TestID.ToString();
            } 
        }
        public ctrlTakeTest()
        {
            InitializeComponent();
        }

        public void LoadTestInfo(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            _TestAppointment = clsTestAppointment.GetTestAppointmentInfoByID(TestAppointmentID);
            _Test = clsTest.GetTestInfoByTestAppointmentID(TestAppointmentID);
            _TestID = (_Test == null) ? -1 : _Test.TestID;

            if(_TestAppointment == null)
            {
                MessageBox.Show($"No Appointment with ID = {_TestAppointmentID.ToString()}"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //incase appointment was found
            lbDrivingLicenseAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lbDrivingClass.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            lbName.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.PersonInfo.FullName;
            lbTrial.Text = clsLocalDrivingLicenseApplication.GetTestTrialsPerTestType(_TestAppointment.LocalDrivingLicenseApplicationID, _TestTypeID).ToString();
            lbDate.Text = _TestAppointment.AppointmentDate.ToString("dd/MMM/yyyy");
            lbFees.Text = clsTestType.GetTestTypeInfoByID(_TestTypeID).TestTypeFees.ToString();
            lbTestID.Text = (_TestID == -1) ? "Not Taken Yet" : _TestID.ToString();
        }

    }
}
