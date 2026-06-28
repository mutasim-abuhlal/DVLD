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

namespace DVLD.Tests.Test_Types
{
    public partial class frmEditTestType : Form
    {
        private clsTestType.enTestTypes _TestTypeID;
        private clsTestType _TestType;

        public frmEditTestType(clsTestType.enTestTypes TestTypeID)
        {
            InitializeComponent();

            _TestTypeID = TestTypeID;
            _TestType = clsTestType.GetTestTypeInfoByID(TestTypeID);
        }

        private void _ResetDefaultValues()
        {
            lbID.Text = "[???]";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtFees.Text = "";
        }

        private void _LoadData()
        {
            lbID.Text = ((int)_TestType.TestTypeID).ToString();
            txtTitle.Text = _TestType.TestTypeTitle;
            txtDescription.Text = _TestType.TestTypeDescription;
            txtFees.Text = Convert.ToInt32(_TestType.TestTypeFees).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "this field cannot be blank!");
            }
            else if (!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid value please enter Numbers only");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFees, null);
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

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "this field cannot be blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtDescription, "");
            }
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            if(_TestType != null)
            {
                _LoadData();
                this.ActiveControl = txtTitle;
            }
            else
            {
                MessageBox.Show($"Could not find Test Type with id = {_TestType.ToString()}");
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("there are some empty fields you have not filled. Please check out them.", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }

            _TestType.TestTypeTitle = txtTitle.Text.Trim();
            _TestType.TestTypeDescription = txtDescription.Text.Trim();
            _TestType.TestTypeFees = Convert.ToSingle(txtFees.Text);

            if(_TestType.Save())
            {
                MessageBox.Show("Data Saved Successfully!", "Confirm!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data did not saved!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
