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

namespace DVLD.Drivers
{
    public partial class ctrlDriverLicenseCard : UserControl
    {
        public clsLicense license { get; set; }

        public ctrlDriverLicenseCard()
        {
            InitializeComponent();
        }

        public void LoadDriverLicenseInfo(int LicenseID)
        {
            license = clsLicense.GetLicenseInfoByID(LicenseID);

            if (license != null)
            {
                lbClass.Text = license.LicenseClassInfo.ClassName;
                lbName.Text = license.ApplicationInfo.PersonInfo.FullName;
                lbLicenseID.Text = license.LicenseID.ToString();
                lbNationalNo.Text = license.ApplicationInfo.PersonInfo.NationalNo;
                lbGendor.Text = license.ApplicationInfo.PersonInfo.GendorCaption;
                lbIssueDate.Text = license.IssueDate.ToString("dd/MMM/yyyy");
                lbIssueReason.Text = license.IssueReasonCaption;
                lbNotes.Text = (license.Notes == "") ? "No Notes" : license.Notes;
                lbIsActive.Text = (license.IsActive) ? "Yes" : "No";
                lbDateOfBirth.Text = license.ApplicationInfo.PersonInfo.DateOfBirth.ToString("dd/MMM/yyyy");
                lbDriverID.Text = license.DriverID.ToString();
                lbExpirationDate.Text = license.ExpirationDate.ToString("dd/MMM/yyyy");
                lbIsDetained.Text = (clsDetainedLicense.IsLicenseDetainedByLicenseID(LicenseID)) ? "Yes" : "No";

                //Handle Driver Image
                if (!string.IsNullOrEmpty(license.ApplicationInfo.PersonInfo.ImagePath))
                    pbDriverImage.ImageLocation = license.ApplicationInfo.PersonInfo.ImagePath;
                else
                {
                    if (license.ApplicationInfo.PersonInfo.Gendor == 0)
                        pbDriverImage.Image = Resources.Male_512;
                    else
                        pbDriverImage.Image = Resources.Female_512;
                }
            }
        }
    }
}
