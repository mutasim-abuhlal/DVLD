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
    public partial class frmDriverLicenseHistory : Form
    {
        private int _DriverID;

        public frmDriverLicenseHistory(int DriverID)
        {
            InitializeComponent();

            this._DriverID = DriverID;
        }

        private void frmDriverLicenseHistory_Load(object sender, EventArgs e)
        {
            int PersonID = clsDriver.GetDriverInfoByID(_DriverID).PersonID;

            ctrlDriverLicenseHistory1.LoadDriverLicenses(_DriverID);
            ctrlPersonInfoWithFilter1.LoadPersonInfo(PersonID);
            ctrlPersonInfoWithFilter1.FilterEnabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
