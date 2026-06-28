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
using UtilityLib;

namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            clsUser user = clsUser.GetUserInfoByUserNameAndPassword(txtUserName.Text,txtPassword.Text);

            if(user == null)
            {
                MessageBox.Show("Invalid Username/Password", "Wrong Credintials!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (user.IsActive)
                {
                    clsGlobal.User = user;
                    if (chbRememberMe.Checked)
                        clsGlobal.SaveCredintialToWindowsRegistry(txtUserName.Text, txtPassword.Text);
                    else
                        clsGlobal.SaveCredintialToWindowsRegistry("", "");

                    this.Hide();
                    Form frm = new frmMain();
                    frm.ShowDialog();
                    this.Show();
                }
                else
                    MessageBox.Show("Your account isn't active contact your admin!", "Not Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

            if (clsGlobal.GetCredintialFromWindowsRegistry(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chbRememberMe.Checked = true;
            }
        }
    }
}
