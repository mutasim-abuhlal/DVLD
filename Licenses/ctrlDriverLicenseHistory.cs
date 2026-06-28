using DVLD_Bussiness;
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
    public partial class ctrlDriverLicenseHistory : UserControl
    {
        private DataTable _dtLocalLicenses;
        private DataTable _dtInternationalLicenses;
        private int _DriverID;

        public ctrlDriverLicenseHistory()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicenses()
        {
            _dtLocalLicenses = clsLicense.GetAllLocalLicensesForDriver(_DriverID);
            dgvLocal.DataSource = _dtLocalLicenses;

            lbLocalRecords.Text = dgvLocal.Rows.Count.ToString();

            if (dgvLocal.Rows.Count > 0)
            {
                dgvLocal.Columns[0].HeaderText = "Lic.ID";
                dgvLocal.Columns[0].Width = 120;

                dgvLocal.Columns[1].HeaderText = "App.ID";
                dgvLocal.Columns[1].Width = 100;

                dgvLocal.Columns[2].HeaderText = "Class Name";
                dgvLocal.Columns[2].Width = 300;

                dgvLocal.Columns[3].HeaderText = "Issue Date";
                dgvLocal.Columns[3].Width = 190;

                dgvLocal.Columns[4].HeaderText = "Expiration Date";
                dgvLocal.Columns[4].Width = 190;

                dgvLocal.Columns[5].HeaderText = "Is Active";
                dgvLocal.Columns[5].Width = 100;
            }
        }

        private void _LoadInternationalLicenses()
        {
            _dtInternationalLicenses = clsInternationalLicense.GetAllInternationalLicensesForDriver(_DriverID);
            dgvInternational.DataSource = _dtInternationalLicenses;

            lbInternationalRecords.Text = dgvInternational.Rows.Count.ToString();

            if (dgvInternational.Rows.Count > 0)
            {
                dgvInternational.Columns[0].HeaderText = "Int.License ID";
                dgvInternational.Columns[0].Width = 160;

                dgvInternational.Columns[1].HeaderText = "Application ID";
                dgvInternational.Columns[1].Width = 120;

                dgvInternational.Columns[2].HeaderText = "L.License ID";
                dgvInternational.Columns[2].Width = 120;

                dgvInternational.Columns[3].HeaderText = "Issue Date";
                dgvInternational.Columns[3].Width = 190;

                dgvInternational.Columns[4].HeaderText = "Expiration Date";
                dgvInternational.Columns[4].Width = 190;

                dgvInternational.Columns[5].HeaderText = "Is Active";
                dgvInternational.Columns[5].Width = 120;
            }
        }

        public void LoadDriverLicenses(int DriverID)
        {
            _DriverID = DriverID;
            clsDriver driver = clsDriver.GetDriverInfoByID(DriverID);

            if(driver == null)
            {
                MessageBox.Show("there is not driver with ID = " + DriverID.ToString(), "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadLocalLicenses();
            _LoadInternationalLicenses();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDriverLicenseInfo((int)dgvLocal.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showInternationalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmInternationalLicenseInfo((int)dgvInternational.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
