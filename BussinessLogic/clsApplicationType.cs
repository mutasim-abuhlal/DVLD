using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsApplicationType
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;

        public enum enApplicationType { NewLicense = 1, RenewLicense = 2, ReplacementForLost = 3, ReplacementForDamage = 4,
            ReleaseDetainedLicense = 5, NewInternationalLicense = 6, RetakeTest = 7 };

        public enApplicationType ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees {  get; set; }

        public clsApplicationType()
        {
            this.ApplicationTypeID = enApplicationType.NewLicense;
            this.ApplicationTypeTitle = "";
            this.ApplicationFees = -1;

            this._Mode = enMode.AddNew;
        }

        private clsApplicationType(enApplicationType ApplicationTypeID,string ApplicationTypeTitle,float ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees=ApplicationFees;

            this._Mode = enMode.Update;
        }

        private bool _AddNewApplicationType()
        {
            return clsApplicationTypeData.AddNewApplicationType(this.ApplicationTypeTitle, this.ApplicationFees) > -1;
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType((int)this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if(_AddNewApplicationType())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdateApplicationType();
            }

            return false;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }

        public static clsApplicationType GetApplicationTypeInfoByID(enApplicationType ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            float ApplicationFees = -1;

            if (clsApplicationTypeData.GetApplicationTypeInfoByID((int)ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            else
                return null;


        }

    }
}
