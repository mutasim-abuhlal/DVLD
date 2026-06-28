using DVLD.Global;
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

namespace DVLD.Tests
{
    public partial class frmScheduelTest : Form
    {

        public frmScheduelTest(clsTestType.enTestTypes TestTypeID, int LocalDrivingLicenseApplicationID,int AppointmentID = -1)
        {
            InitializeComponent();

            ctrlScheduleTest1.TestTypeID = TestTypeID;
            ctrlScheduleTest1.LoadInfo(LocalDrivingLicenseApplicationID,AppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
