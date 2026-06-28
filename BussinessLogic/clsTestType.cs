using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsTestType
    {
        private enum enMode { AddNew,Update};
        private enMode _Mode;

        public enum enTestTypes { Vision = 1,Written = 2,Practical = 3};

        public enTestTypes TestTypeID { get; set; }
        public string TestTypeTitle {  get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees {  get; set; }

        public clsTestType()
        {
            TestTypeID = enTestTypes.Vision;
            TestTypeTitle = "";
            TestTypeDescription = "";
            TestTypeFees = -1;

            _Mode = enMode.AddNew;
        }

        private clsTestType(enTestTypes TestTypeID,string TestTypeTitle,string TestTypeDescription,float TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;

            this._Mode = enMode.Update;
        }

        private bool _AddNewTestType()
        {
            this.TestTypeID = (enTestTypes)clsTestTypeData.AddNewTestType(this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);

            return this.TestTypeTitle != "";
        }

        private bool _UpdateTestType()
        {
            return clsTestTypeData.UpdateTestType((int)this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }

        public bool Save()
        {
            switch(this._Mode)
            {
                case enMode.AddNew:
                    if(_AddNewTestType())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdateTestType();
            }

            return false;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }

        public static clsTestType GetTestTypeInfoByID(enTestTypes TestTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription = "";
            float TestTypeFees = -1;

            if (clsTestTypeData.GetTestTypeInfoByID((int)TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            else
                return null;
        }
    }
}
