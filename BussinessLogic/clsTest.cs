using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsTest
    {
        private enum enMode { AddNew,Update}
        private enMode _Mode;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser UserInfo { get; set; }

        public clsTest()
        {
            TestID = -1;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = "";
            CreatedByUserID = -1;

            this._Mode = enMode.AddNew;
        }

        public clsTest(int TestID,int TestAppointmentID,bool TestResult,string Notes,int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.GetTestAppointmentInfoByID(this.TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUser.GetUserInfoByID(this.CreatedByUserID);

            this._Mode = enMode.Update;
        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);

            return this.TestID > -1;
        }

        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch(this._Mode)
            {
                case enMode.AddNew:
                    if(_AddNewTest())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
                    case enMode.Update:
                    return _UpdateTest();
            }

            return false;
        }

        public static DataTable GetAllTests()
        {
            return clsTestData.GetAllTests();
        }

        public static DataTable GetAllTestsForOnePerson(int PersonID)
        {
            return clsTestData.GetAllTestsForOneApplication(PersonID);
        }

        public static clsTest GetTestInfoByID(int TestID)
        {
            int TestAppointment = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;

            if (clsTestData.GetTestInfoByID(TestID, ref TestAppointment, ref TestResult, ref Notes, ref CreatedByUserID))
                return new clsTest(TestID, TestAppointment, TestResult, Notes, CreatedByUserID);
            else
                return null;
        }

        public static clsTest GetTestInfoByTestAppointmentID(int TestAppointmentID)
        {
            int TestID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;

            if (clsTestData.GetTestInfoByTestAppointmentID(TestAppointmentID, ref TestID, ref TestResult, ref Notes, ref CreatedByUserID))
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            else
                return null;
        }

        public static bool IsTestExist(int TestID)
        {
            return clsTestData.IsTestExist(TestID);
        }

        public static bool DoesPersonPassedTest(int LocalDrivingLicenseApplicationID,clsTestType.enTestTypes TestTypeID)
        {
            return clsTestData.DoesPassTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
    }
}
