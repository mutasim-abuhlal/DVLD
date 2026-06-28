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
    public partial class ctrlUserCard : UserControl
    {
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public bool LoadUserInfo(int UserID)
        {
            clsUser user = clsUser.GetUserInfoByID(UserID);

            if (user != null)
            {
                ctrlPersonInfo1.LoadPersonInfo(user.PersonID);
                lbUserID.Text = user.UserID.ToString();
                lbUserName.Text = user.UserName;
                lbIsActive.Text = (user.IsActive) ? "Yes" : "No";
                return true;
            }
            else
                return false;
        }
    }
}
