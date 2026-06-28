using DVLD_Bussiness;
using DVLD.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmListUsers : Form
    {
        private DataTable _dtUsers;

        public frmListUsers()
        {
            InitializeComponent();
        }

        private void _RefreshUsers()
        {
            _dtUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtUsers;

            lbTotalRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _RefreshUsers();

            if(dgvUsers.Rows.Count> 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 120;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 120;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 300;

                dgvUsers.Columns[3].HeaderText = "UserName";
                dgvUsers.Columns[3].Width = 140;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 120;
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Is Active");
            cbIsActive.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text == "Is Active");

            if(txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            if (cbIsActive.Visible)
            {
                cbIsActive.SelectedIndex = 0;
                cbIsActive.Focus();
            }

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch(cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "User name":
                    FilterColumn = "UserName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if(txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtUsers.DefaultView.RowFilter = "";
                lbTotalRecords.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            if(FilterColumn == "UserID" || FilterColumn == "PersonID")
                _dtUsers.DefaultView.RowFilter = string.Format("{0} = {1}",FilterColumn,txtFilterValue.Text);
            else
                _dtUsers.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterColumn, txtFilterValue.Text);

            lbTotalRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsActive.Text == "All")
            {
                _dtUsers.DefaultView.RowFilter = "";
                lbTotalRecords.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            if (cbIsActive.Text == "Yes")
                _dtUsers.DefaultView.RowFilter = string.Format("{0} = 1","IsActive");
            else
                _dtUsers.DefaultView.RowFilter = string.Format("{0} = 0","IsActive");

            lbTotalRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser();
            frm.ShowDialog();

            _RefreshUsers();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshUsers();
        }

        private void showUserDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUserDetails((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser();
            frm.ShowDialog();

            this._RefreshUsers();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;


            if (MessageBox.Show($"Are you sure you want to delete User [{UserID}]", "Confirm delete!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (clsUser.DeleteUser(UserID))
                    MessageBox.Show("User has been Deleted Successfully", "Confirm!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show($"Cannot delete user with ID = {UserID} due to he has data linked to him!",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
            this._RefreshUsers();
        }

        private void toolStripChangePasswordMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this feature not Implemented yet!", "Not Implemented!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void callPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this feature not Implemented yet!", "Not Implemented!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
