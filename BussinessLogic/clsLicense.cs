using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsLicense
    {
        private enum enMode { AdNew,Update}
        private enMode _Mode;

        public enum enIssueReason {FirstTime = 1,Renew=2,ReplacementForDamaged = 3,ReplacementForLost = 4};

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clsApplication ApplicationInfo { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }
        public clsLicenseClass.enLicenseClasses LicenseClass { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }

        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }

        public string IssueReasonCaption 
        { 
            get 
            { 
                switch(IssueReason)
                {
                    case enIssueReason.FirstTime:
                        return "First Time";
                    case enIssueReason.Renew:
                        return "Renew";
                    case enIssueReason.ReplacementForLost:
                        return "Remplacement For Lost";
                    case enIssueReason.ReplacementForDamaged:
                        return "Replacement For Damaged";

                }

                return "";
            } 
        }
        public int CreatedByUserID { get; set; }
        public clsUser UserInfo { get; set; }

        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            LicenseClass = clsLicenseClass.enLicenseClasses.SmallAndMediumBus;
            IssueDate = DateTime.MinValue;
            ExpirationDate = DateTime.MinValue;
            Notes = "";
            PaidFees = -1;
            IsActive = false;
            IssueReason = enIssueReason.FirstTime;
            CreatedByUserID = -1;

            this._Mode = enMode.AdNew;
        }

        private clsLicense(int LicenseID,int ApplicationID,int DriverID,clsLicenseClass.enLicenseClasses LicenseClass,
            DateTime IssueDate,DateTime ExpirationDate,string Notes,float PaidFees,bool IsActive,enIssueReason IssueReason,int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = clsApplication.GetApplicationInfoByID(this.ApplicationID);
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.GetDriverInfoByID(this.DriverID);
            this.LicenseClass = LicenseClass;
            this.LicenseClassInfo = clsLicenseClass.GetLicenseClassInfoByID(this.LicenseClass);
            this.IssueDate = IssueDate;
            this.IssueReason = IssueReason;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUser.GetUserInfoByID(this.CreatedByUserID);

            this._Mode = enMode.Update;
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, (int)this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes,
                this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);

            return this.LicenseID > -1;
        }

        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, (int)this.LicenseClass, this.IssueDate, this.ExpirationDate,
                this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch(this._Mode)
            {
                case enMode.AdNew:
                    if(_AddNewLicense())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdateLicense();
            }

            return false;
        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();
        }

        public static DataTable GetAllLocalLicensesForDriver(int DriverID)
        {
            return clsLicenseData.GetAllLocalLicensesForDriver(DriverID);
        }

        public static clsLicense GetLicenseInfoByID(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClassID = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            string Notes = "";
            float PaidFees = -1;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = -1;

            if (clsLicenseData.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClassID, ref IssueDate,
                ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
                return new clsLicense(LicenseID, ApplicationID, DriverID, (clsLicenseClass.enLicenseClasses)LicenseClassID,
                    IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;
        }

        public static clsLicense GetActiveLicenseInfoByDriverID(int DriverID,clsLicenseClass.enLicenseClasses LicenseClassID)
        {
            int ApplicationID = -1, LicenseID = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            string Notes = "";
            float PaidFees = -1;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = -1;

            if (clsLicenseData.GetActiveLicenseInfoByDriverID(DriverID,(int)LicenseClassID, ref LicenseID, ref ApplicationID, ref IssueDate, ref ExpirationDate,
                ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees,
                    IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;
        }

        public static bool DeleteLicense(int LicenseID)
        {
            return clsLicenseData.DeleteLicense(LicenseID);
        }

        public static bool IsLicenseExist(int LicenseID)
        {
            return clsLicenseData.IsLicenseExist(LicenseID);
        }

        public static bool DoesPersonHaveLicenseWithTheSameAppliedLicenseClass(int PersonID,clsLicenseClass.enLicenseClasses LicenseClassID)
        {
            return clsLicenseData.DoesPersonHaveLicenseWithTheSameAppliedLicenseClass(PersonID, (int)LicenseClassID);
        }

        public static int GetActiveLicenseID(int PersonID,clsLicenseClass.enLicenseClasses LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseID(PersonID, (int)LicenseClassID);
        }
    }
}
