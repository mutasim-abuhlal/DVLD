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
    public partial class frmInternationalLicenseInfo : Form
    {
        private int _InternationalLicenseID;

        public frmInternationalLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();

            this._InternationalLicenseID = InternationalLicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlInternationalLicenseCard1.LoadInternationalLicense(_InternationalLicenseID);
        }
    }
}
