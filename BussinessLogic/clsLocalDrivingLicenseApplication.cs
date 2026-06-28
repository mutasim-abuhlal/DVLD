using DVLD_DataAccess;
using DVLD_Bussiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        //private enum enMode { AddNew, Update}
        //private enMode _Mode;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsLicenseClass.enLicenseClasses LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }

        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            LicenseClassID = clsLicenseClass.enLicenseClasses.SmallMotorcycle;

            this._Mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID,int ApplicationID, clsLicenseClass.enLicenseClasses LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.GetLicenseClassInfoByID(this.LicenseClassID);

            clsApplication application = clsApplication.GetApplicationInfoByID(this.ApplicationID);

            if (application != null)
            {
                this.ApplicantPersonID = application.ApplicantPersonID;
                this.PersonInfo = application.PersonInfo;
                this.ApplicationDate = application.ApplicationDate;
                this.ApplicationTypeID = application.ApplicationTypeID;
                this.ApplicationTypeInfo = application.ApplicationTypeInfo;
                this.ApplicationStatus = application.ApplicationStatus;
                this.LastStatusDate = application.LastStatusDate;
                this.PaidFees = application.PaidFees;
                this.CreatedByUserID = application.CreatedByUserID;
                this.UserInfo = application.UserInfo;
            }

           

            this._Mode = enMode.Update;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, (int)this.LicenseClassID);

            return this.LocalDrivingLicenseApplicationID > -1;
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, (int)this.LicenseClassID);
        }

        public bool Save()
        {
            switch(this._Mode)
            {
                case enMode.AddNew:
                    if(base.Save() && this._AddNewLocalDrivingLicenseApplication())
                    {
                        //this._Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return   base.Save() && _UpdateLocalDrivingLicenseApplication();
            }

            return false;
        }

        public static DataTable GetAllLocalDrivingLicenesApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public static clsLocalDrivingLicenseApplication GetLocalDrivingLicenseApplicationByID(int LocalDrivingLicenseApplciationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplciationID, ref ApplicationID, ref LicenseClassID))
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplciationID, ApplicationID, (clsLicenseClass.enLicenseClasses)LicenseClassID);
            else
                return null;
        }

        public static clsLocalDrivingLicenseApplication GetLocalDrivingLicenseApplicationInfoByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplciationID = -1, LicenseClassID = -1;

            if(clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID,ref LocalDrivingLicenseApplciationID, ref LicenseClassID))
                 return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplciationID, ApplicationID, (clsLicenseClass.enLicenseClasses)LicenseClassID);
            else
                return null;
        }

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
        }

        public static bool IsLocalDrivingLicenseApplicationExist(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.IsLocalDrivingLicenseApplicationExist(LocalDrivingLicenseApplicationID);
        }

        public static int DoesPersonHaveAnActiveApplicationWithSameLicenseClass(int PersonID, int LicenseClassID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPersonHaveAnActiveApplicationWithSameLicenseClass(PersonID, LicenseClassID);
        }

        public int GetPassedTestCount()
        {
            return clsLocalDrivingLicenseApplicationData.GetPassedTests(this.LocalDrivingLicenseApplicationID);
        }

        public static int GetPassedTests(int LocalDrivingLicenseApplciationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetPassedTests(LocalDrivingLicenseApplciationID);
        }

        public static int GetTestTrialsPerTestType(int LocalDrivingLicenseApplicationID,clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.GetTotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool DoesAttnedTestType(int LocalDrivingLicenseApplicationID,clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttnedTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttnedTestType(clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttnedTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID,clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesPassTestType(clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
    }
}
