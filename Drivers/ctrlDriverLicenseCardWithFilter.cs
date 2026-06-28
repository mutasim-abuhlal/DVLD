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
    public partial class ctrlDriverLicenseCardWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;


        public bool FilterEnabled { get { return groupBox1.Enabled; }set { groupBox1.Enabled = value; } }    
        public ctrlDriverLicenseCardWithFilter()
        {
            InitializeComponent();
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if(e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
        }
        public void LoadInfo(int LicenseID)
        {
            txtFilterValue.Text = LicenseID.ToString();
            btnFind_Click(null, null);
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            int LicenseID = Convert.ToInt32(txtFilterValue.Text);
            ctrlDriverLicenseCard1.LoadDriverLicenseInfo(LicenseID);
            if(OnLicenseSelected != null && FilterEnabled)
                OnLicenseSelected(LicenseID);
        }

        public void FilterFoucs()
        {
            this.ActiveControl = txtFilterValue;
        }
    }
}
