using DVLD.Licenses.International_Licenses;
using DVLD.Licenses.Local_Licenses;
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
using DVLD.Drivers;

namespace DVLD.Applications
{
    public partial class frmListInternationalLicenseApplications : Form
    {
        private DataTable _dtInternationalLicenses;
        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void _RefreshInternationalLicenses()
        {
            _dtInternationalLicenses = 
                clsInternationalLicense.
                GetAllInternationalLicenses().DefaultView.ToTable(false,"InternationalLicenseID","ApplicationID",
                "DriverID","IssuedUsingLocalLicenseID","IssueDate","ExpirationDate","IsActive");
            dgvInternationalLicenses.DataSource = _dtInternationalLicenses;

            lbTotalRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddInternationalLicense();
            frm.ShowDialog();

            _RefreshInternationalLicenses();
        }

        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _RefreshInternationalLicenses();

            if(dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 160;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 160;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 140;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 140;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 160;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 160;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 160;
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Int.License ID" || cbFilterBy.Text == "Application ID" ||
                cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "L.License ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Is Active");
            cbIsActive.Visible = (cbFilterBy.Text == "Is Active");

            if(txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                this.ActiveControl = txtFilterValue;
            }

            if(cbIsActive.Visible)
            {
                cbIsActive.SelectedIndex = 0;
                this.ActiveControl = cbIsActive;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch(cbFilterBy.Text)
            {
                case "Int.License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "L.License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if(txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalLicenses.DefaultView.RowFilter = "";
                lbTotalRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "InternationalLicenseID" || FilterColumn == "ApplicationID" ||
                FilterColumn == "DriverID" || FilterColumn == "IssuedUsingLocalLicenseID")
                _dtInternationalLicenses.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, txtFilterValue.Text);
            else
                _dtInternationalLicenses.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterColumn, txtFilterValue.Text);

            lbTotalRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriver.GetDriverInfoByID((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value).PersonID;

            Form frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmInternationalLicenseInfo((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDriverLicenseHistory((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value);
            frm.ShowDialog();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbIsActive.Text)
            {
                case "All":
                    _dtInternationalLicenses.DefaultView.RowFilter = "";
                    break;
                case "Yes":
                    _dtInternationalLicenses.DefaultView.RowFilter = string.Format("IsActive = 1");
                    break;
                case "No":
                    _dtInternationalLicenses.DefaultView.RowFilter = string.Format("IsActive = 0");
                    break;
            }

            lbTotalRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }
    }
}
