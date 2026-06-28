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

namespace DVLD.Users
{
    public partial class frmAddEditUser : Form
    {
        private enum enMode { AddNew,Update}
        private enMode _Mode;

        private int _UserID;
        private int _PersonID = -1;
        private clsUser _User;

        public frmAddEditUser()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
            _User = new clsUser();
            _UserID = -1;
        }

        public frmAddEditUser(int UserID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _User = clsUser.GetUserInfoByID(UserID);
            _UserID = UserID;
            _PersonID = _User.PersonID;
        }

        private void _ResetDefaultValues()
        {

            lbUserID.Text = "[???]";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";

            txtUserName.Focus();
        }

        private void _LoadData()
        {
            lbScreenHeader.Text = "  Update User";
            lbUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = "";

            ctrlPersonInfoWithFilter1.LoadPersonInfo(_User.PersonID);
            ctrlPersonInfoWithFilter1.FilterEnabled = false;
        }

        //Events

        private void ctrlPersonInfoWithFilter1_OnPersonSelected(int PersonID)
        {
            _PersonID = PersonID;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this._Mode != enMode.Update)
            {
                if (this._PersonID == -1)
            {
                MessageBox.Show("Select a Person", "Not Selected!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

                if (clsUser.IsUserExistByPersonID(this._PersonID))
                {
                    MessageBox.Show("Person already is user, Choose another one.", "Select another person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //in case update mode
            tabControl1.SelectedTab = tabControl1.TabPages["tpUserInfo"];
            btnSave.Enabled = true;
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if(_Mode == enMode.Update)
            {
                if (_User != null)
                {
                    _LoadData();
                }
                else
                    MessageBox.Show("Invalid user please select a valid one", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "this field cannot be blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, null);
            }

            if (_User.UserName != txtUserName.Text)
                if (clsUser.IsUserExist(txtUserName.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "User name is used by another user.\nChoose another one");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtUserName, null);
                }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "this field cannot be blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirmation does not match password!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("there are some empty fields you have not filled. Please check out them.", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }

            _User.UserName = txtUserName.Text;
            _User.PersonID = this._PersonID;
            _User.Password = txtPassword.Text;
            _User.IsActive = chbIsActive.Checked;

            if(_User.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Confirm!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update;
                lbScreenHeader.Text = "  Update User";
                lbUserID.Text = _User.UserID.ToString();
            }
            else
                MessageBox.Show("Data Did not Saved", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
