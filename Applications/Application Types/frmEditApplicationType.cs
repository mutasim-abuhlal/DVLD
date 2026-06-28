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

namespace DVLD.Applications.Application_Types
{
    public partial class frmEditApplicationType : Form
    {
        private clsApplicationType.enApplicationType _ApplicationTypeID;
        private clsApplicationType _Type;

        public frmEditApplicationType(clsApplicationType.enApplicationType ApplicationTypeID)
        {
            InitializeComponent();

            _ApplicationTypeID = ApplicationTypeID;
            _Type = clsApplicationType.GetApplicationTypeInfoByID(_ApplicationTypeID);
        }

        private void _LoadData()
        {
            if(_Type != null)
            {
                lbID.Text = ((int)_ApplicationTypeID).ToString();
                txtTitle.Text = _Type.ApplicationTypeTitle;
                txtFees.Text = Convert.ToInt32(_Type.ApplicationFees).ToString();
            }
        }

        private void _ResetDefaultValues()
        {
            lbID.Text = "[???]";
            txtTitle.Text = "";
            txtFees.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _LoadData();
            this.ActiveControl = txtTitle;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("there are some empty fields you have not filled. Please check out them.", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }

            _Type.ApplicationTypeTitle = txtTitle.Text.Trim();
            _Type.ApplicationFees = Convert.ToSingle(txtFees.Text.Trim());

            if(_Type.Save())
            {
                MessageBox.Show("Data Saved Successfully!", "Confirm!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data did not saved!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "this field cannot be blank!");
            }
            else if(!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid value please enter Numbers only");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFees, "");
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "this field cannot be blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTitle, "");
            }
        }
    }
}
