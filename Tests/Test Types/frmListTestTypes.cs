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
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtTestTypes;

        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void _RefreshTestTypes()
        {
            _dtTestTypes = clsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtTestTypes;

            lbTotalRecords.Text = dgvTestTypes.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestTypes();

            if(dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 130;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 170;

                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 300;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 130;
            }
        }

        private void EditTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmEditTestType((clsTestType.enTestTypes)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            this._RefreshTestTypes();
        }
    }
}
