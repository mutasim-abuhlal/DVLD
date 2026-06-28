using DVLD.Drivers;
using DVLD.Global;
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

namespace DVLD.Licenses.Detained_License
{
    public partial class frmDetainedLicense : Form
    {
        private int _LicenseID;
        private clsLicense _License;

        public frmDetainedLicense()
        {
            InitializeComponent();
        }

        private void frmAddDetainLicense_Load(object sender, EventArgs e)
        {
            lbDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lbCreatedBy.Text = clsGlobal.User.UserID.ToString();
        }

        private void ctrlDriverLicenseCardWithFilter1_OnLicenseSelected(int LicenseID)
        {
            this._License = clsLicense.GetLicenseInfoByID(LicenseID);
            this._LicenseID = LicenseID;

            if(_License == null)
            {
                MessageBox.Show("No License with ID = " + LicenseID.ToString(), "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return;
            }

            //incase license was found 
            if (_HandleWhenSelectedLicenseWasDetained())
                return;

            lbLicenseID.Text = LicenseID.ToString();
            btnDetain.Enabled = true;
            lkbShowLicenseHistory.Enabled = true;
            txtFineFees.Focus();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("there are some empty fildes please fill them\nby hold on red icon(s).", "Not Allowed"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsDetainedLicense Detain = new clsDetainedLicense();

            Detain.LicenseID = this._License.LicenseID;
            Detain.DetainDate = DateTime.Now;
            Detain.FineFees = Convert.ToSingle(txtFineFees.Text);
            Detain.CreatedByUserID = clsGlobal.User.UserID;
            Detain.IsReleased = false;

            if (MessageBox.Show("Are you sure you want to Detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Detain.Save())
                {
                    MessageBox.Show($"License Detained Successfully with ID = {Detain.DetainID}", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbDetainID.Text = Detain.DetainID.ToString();
                    lkbShowLicenseInfo.Enabled = true;
                    ctrlDriverLicenseCardWithFilter1.FilterEnabled = false;
                    btnDetain.Enabled = false;
                    txtFineFees.Enabled = false;
                }
                else
                    MessageBox.Show("data did not saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkbShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmDriverLicenseHistory(this._License.DriverID);
            frm.ShowDialog();
        }

        private void lkbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmDriverLicenseInfo(this._License.LicenseID);
            frm.ShowDialog();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (!Single.TryParse(txtFineFees.Text, out float _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid value pelase enter number only");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFineFees, "");
            }
        }

        private void frmDetainedLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseCardWithFilter1.FilterFoucs();
        }

        private bool _HandleWhenSelectedLicenseWasDetained()
        {
            if (clsDetainedLicense.IsLicenseDetainedByLicenseID(this._LicenseID))
            {
                MessageBox.Show("Selected license is already detained!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return true;
            }

            return false;
        }
    }
}
