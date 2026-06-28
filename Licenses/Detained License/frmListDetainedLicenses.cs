using System;
using DVLD.Applications.Release_License;
using DVLD.People;
using DVLD.Licenses;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Bussiness;
using System.Diagnostics.Eventing.Reader;
using DVLD.Drivers;

namespace DVLD.Licenses.Detained_License
{
    public partial class frmListDetainedLicenses : Form
    {
        private DataTable _DetainedLicenses;

        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void _RefreshDetainedLicenses()
        {
            _DetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = _DetainedLicenses;

            lbTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetainedLicense();
            frm.ShowDialog();

            _RefreshDetainedLicenses();
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseLicense();
            frm.ShowDialog();

            _RefreshDetainedLicenses();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _RefreshDetainedLicenses();
            
            if(dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 70;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 70;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 100;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 120;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No";
                dgvDetainedLicenses.Columns[6].Width = 100;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 300;

                dgvDetainedLicenses.Columns[8].HeaderText = "Release App.ID";
                dgvDetainedLicenses.Columns[8].Width = 130;
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "License ID"
                || cbFilterBy.Text == "Release App.ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Is Released");
            cbIsReleased.Visible = (cbFilterBy.Text == "Is Released");

            if(txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                this.ActiveControl = txtFilterValue;
            }

            if(cbIsReleased.Visible)
            {
                cbIsReleased.SelectedIndex = 0;
                this.ActiveControl = cbIsReleased;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch(cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "License ID":
                    FilterColumn = "LicenseID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Release App.ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if(txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _DetainedLicenses.DefaultView.RowFilter = "";
                lbTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
                return;
            }

            if(FilterColumn == "DetainID" || FilterColumn == "LicenseID" || FilterColumn == "ReleaseApplicationID")
                _DetainedLicenses.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, txtFilterValue.Text);
            else
                _DetainedLicenses.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterColumn, txtFilterValue.Text);

            lbTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbIsReleased.Text)
            {
                case "All":
                    _DetainedLicenses.DefaultView.RowFilter = "";
                    break;
                case "Yes":
                    _DetainedLicenses.DefaultView.RowFilter = string.Format("{0} = {1}", "IsReleased", "1");
                    break;
                case "No":
                    _DetainedLicenses.DefaultView.RowFilter = string.Format("{0} = {1}", "IsReleased", "0");
                    break;
            }

            lbTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsLicense.GetLicenseInfoByID((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value).DriverInfo.PersonID;

            Form frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDriverLicenseInfo((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDriverLicenseHistory(clsLicense.GetLicenseInfoByID((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value).DriverID);
            frm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)dgvDetainedLicenses.CurrentRow.Cells[3].Value;
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseLicense((int)dgvDetainedLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshDetainedLicenses();
        }
    }
}
