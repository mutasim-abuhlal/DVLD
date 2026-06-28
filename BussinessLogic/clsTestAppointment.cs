using DVLD_Bussiness;
using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsTestAppointment
    {
        private enum enMode { AddNew,Update};
        private enMode _Mode;

        public int TestAppointmentID { get; set; }
        public clsTestType.enTestTypes TestTypeID { get; set; }
        public clsTestType TestTypeInfo { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser UserInfo { get; set; }
        public bool IsLocked { get; set; }
        public int? RetakeApplicationID { get; set; }
        public clsApplication RetakeApplicationInfo { get; set; }

        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = clsTestType.enTestTypes.Vision;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.MinValue;
            PaidFees = -1;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeApplicationID = null;

            _Mode = enMode.AddNew;
        }

        private clsTestAppointment(int TestAppointmentID,clsTestType.enTestTypes TestTypeID,int LocalDrivingLicenseApplicationID,DateTime AppointmentDate,
            float PaidFees,int CreatedByUserID,bool IsLocked,int ? RetakeApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.TestTypeInfo = clsTestType.GetTestTypeInfoByID(this.TestTypeID);
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(this.LocalDrivingLicenseApplicationID);
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUser.GetUserInfoByID(this.CreatedByUserID);
            this.IsLocked = IsLocked;
            this.RetakeApplicationID = RetakeApplicationID;

            if (this.RetakeApplicationID != null)
                this.RetakeApplicationInfo = clsApplication.GetApplicationInfoByID((int)this.RetakeApplicationID);

            _Mode = enMode.Update;
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate,
                this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeApplicationID);

            return this.TestAppointmentID > -1;
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate,
                this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeApplicationID);
        }

        public bool Save()
        {
            switch(this._Mode)
            {
                case enMode.AddNew:
                    if(_AddNewTestAppointment())
                    {
                        this._Mode = enMode.AddNew;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdateTestAppointment();
            }

            return false;
        }

        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentData.GetAllTestAppointments();
        }

        public static DataTable GetAllPersonTestAppointmentsForGivenTestType(int LocalDrivingLicenseApplicationID,clsTestType.enTestTypes TestTypeID)
        {
            return clsTestAppointmentData.GetAllPersonTestAppointmentsForGivenTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static clsTestAppointment GetTestAppointmentInfoByID(int TestAppointmentID)
        {
            int TestTypeID = -1, LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.MinValue;
            float PaidFees = -1;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int? RetakeApplicationID = null;

            if (clsTestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeApplicationID))
                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestTypes)TestTypeID,
                    LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeApplicationID);
            else
                return null;
        }

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            return clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID);
        }

        public static bool IsTestAppointmentExist(int TestAppointmentID)
        {
            return clsTestAppointmentData.IsTestAppointmentIDExist(TestAppointmentID);
        }

        public static bool DoesPersonHaveAnActiveAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestTypes TestTypeID)
        {
            return clsTestAppointmentData.DoesApplicationHaveAnActiveAppointment(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
    }
}
