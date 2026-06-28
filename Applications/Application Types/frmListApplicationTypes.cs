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
    public partial class frmListApplicationTypes : Form
    {
        private DataTable _dtApplicationTypes;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void _RefershApplicationTypes()
        {
            _dtApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtApplicationTypes;

            lbTotalRecords.Text = dgvApplicationTypes.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefershApplicationTypes();

            if(dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 130;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 300;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 130;
            }
        }

        private void EditApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmEditApplicationType((clsApplicationType.enApplicationType)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            this._RefershApplicationTypes();
        }
    }
}
