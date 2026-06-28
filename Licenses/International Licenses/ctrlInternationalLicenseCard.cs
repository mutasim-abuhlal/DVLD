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

namespace DVLD.Licenses.International_Licenses
{
    public partial class ctrlInternationalLicenseCard : UserControl
    {
        public ctrlInternationalLicenseCard()
        {
            InitializeComponent();
        }

        public void LoadInternationalLicense(int IntLicenseID)
        {
            clsInternationalLicense IntLicense = clsInternationalLicense.GetInternationalLicenseInfoByID(IntLicenseID);

            if (IntLicense != null)
            {
                lbName.Text = IntLicense.DriverInfo.PersonInfo.FullName;
                lbInternationalLicenseID.Text = IntLicense.InternationalLicenseID.ToString();
                lbLicenseID.Text = IntLicense.IssuedUsingLocalLicenseID.ToString();
                lbNationalNo.Text = IntLicense.DriverInfo.PersonInfo.NationalNo;
                lbGendor.Text = IntLicense.DriverInfo.PersonInfo.GendorCaption;
                lbIssueDate.Text = IntLicense.IssueDate.ToString("dd/MMM/yyyy");
                lbApplicationID.Text = IntLicense.ApplicationID.ToString();
                lbIsActive.Text = (IntLicense.IsActive) ? "Yes" : "No";
                lbDateOfBirth.Text = IntLicense.DriverInfo.PersonInfo.DateOfBirth.ToString("dd/MMM/yyyy");
                lbDriverID.Text = IntLicense.DriverID.ToString();
                lbEpxirationDate.Text = IntLicense.ExpirationDate.ToString("dd/MMM/yyyy");

                if (!string.IsNullOrEmpty(IntLicense.DriverInfo.PersonInfo.ImagePath))
                    pbDriverImage.ImageLocation = IntLicense.DriverInfo.PersonInfo.ImagePath;
                else
                {
                    if (IntLicense.DriverInfo.PersonInfo.Gendor == 0)
                        pbDriverImage.Image = Resources.Male_512;
                    else
                        pbDriverImage.Image = Resources.Female_512;
                }
            }
            else
                MessageBox.Show("Selected International License was not Found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
