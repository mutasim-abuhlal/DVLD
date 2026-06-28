using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsLicenseClass
    {
        private enum enMode { AddNew,Update};
        private enMode _Mode;

        public enum enLicenseClasses { SmallMotorcycle = 1,HeavyMotorcycle = 2,Ordinary = 3,Commercial = 4,Agricultural = 5,
        SmallAndMediumBus = 6,TruckAndHeavyVehicle = 7};

        public enLicenseClasses LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }

        public clsLicenseClass()
        {
            this.LicenseClassID = enLicenseClasses.SmallMotorcycle;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = -1;
            this.DefaultValidityLength = -1;
            this.ClassFees = -1;

            this._Mode = enMode.AddNew;
        }

        private clsLicenseClass(enLicenseClasses LicenseClassID,string ClassName,
            string ClassDescription,int MinimumAllowedAge,int DefaultValidityLength,float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;

            this._Mode = enMode.Update;
        }

        private bool _AddNewLicenseClass()
        {
            return clsLicenseClassData.AddNewLicenseClass(this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees) > -1;
        }

        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassData.UpdateLicenseClass((int)this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }

        public bool Save()
        {
            switch(this._Mode)
            {
                case enMode.AddNew:
                    if(_AddNewLicenseClass())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                   return _UpdateLicenseClass();
            }

            return false;
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }

        public static clsLicenseClass GetLicenseClassInfoByID(enLicenseClasses LicenseClassID)
        {
            string ClassName = "", ClassDescription = "";
            int MinimumAllowedAge = -1, DefaultValidityLength = -1;
            float ClassFees = -1;

            if (clsLicenseClassData.GetLicenseClassInfoByID((int)LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }

        public static clsLicenseClass GetLicenseClassInfoByClassName(string ClassName)
        {
            int LicenseClassID = -1;
            string ClassDescription = "";
            int MinimumAllowedAge = -1;
            int DefaultValidityLength = -1;
            float ClassFees = -1;

            if (clsLicenseClassData.GetLicenseClassInfoByClassName(ClassName, ref LicenseClassID,ref ClassDescription, ref MinimumAllowedAge,
                ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass((clsLicenseClass.enLicenseClasses)LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }

    }
}
