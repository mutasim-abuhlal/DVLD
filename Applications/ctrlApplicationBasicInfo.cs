using DVLD_Bussiness;
using DVLD.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        clsApplication _App;
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _App = clsApplication.GetApplicationInfoByID(ApplicationID);

            if(_App != null)
            {
                lbID.Text         = _App.ApplicationID.ToString();
                lbStatus.Text     = _App.StatusCaption;
                lbFees.Text       = _App.PaidFees.ToString();
                lbType.Text       = _App.ApplicationTypeInfo.ApplicationTypeTitle;
                lbApplicant.Text  = _App.PersonInfo.FullName;
                lbDate.Text       = _App.ApplicationDate.ToString("dd/MMM/yyyy");
                lbStatusDate.Text = _App.LastStatusDate.ToString("dd/MMM/yyyy");
                lbCreatedBy.Text  = _App.UserInfo.UserName;
            }
        }

        private void lkbPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmPersonDetails(_App.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
