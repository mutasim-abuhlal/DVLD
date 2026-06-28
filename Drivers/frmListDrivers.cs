using DVLD_Bussiness;
using DVLD.People;
using DVLD.Licenses.International_Licenses;
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
    public partial class frmListDrivers : Form
    {
        private DataTable _dtDrivers;

        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void _RefreshDrivers()
        {
            _dtDrivers = clsDriver.GetAllDrivers();
            dgvDrivers.DataSource = _dtDrivers;

            lbTotalRecords.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _RefreshDrivers();

            if(dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 120;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 120;

                dgvDrivers.Columns[2].HeaderText = "National No.";
                dgvDrivers.Columns[2].Width = 160;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 350;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 180;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 160;

            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Driver ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if(txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                this.ActiveControl = txtFilterValue;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch(cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if(FilterColumn == "None" || txtFilterValue.Text.Trim() == "")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                lbTotalRecords.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "DriverID" || FilterColumn == "PersonID")
                _dtDrivers.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, txtFilterValue.Text);
            else
                _dtDrivers.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterColumn, txtFilterValue.Text);

            lbTotalRecords.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmPersonDetails((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDriverLicenseHistory((int)dgvDrivers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

    }
}
