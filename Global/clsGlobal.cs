using DVLD_Bussiness;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Configuration;

namespace DVLD.Global
{
    internal static class clsGlobal
    {
        public static clsUser User;

        public readonly static string AppName = ConfigurationManager.AppSettings["AppName"];

        private readonly static string UserNameValueName = "UserName";
        private readonly static string PasswordValueName = "Password";

        public static void SaveCredintialToWindowsRegistry(string UserName,string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

            try
            {
                Registry.SetValue(KeyPath, UserNameValueName, UserName, RegistryValueKind.String);
                Registry.SetValue(KeyPath, PasswordValueName, Password, RegistryValueKind.String);
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"An error ocurred : {ex.Message}","Error!",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public static bool GetCredintialFromWindowsRegistry(ref string UserName,ref string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

            try
            {
                UserName = (string)Registry.GetValue(KeyPath, UserNameValueName, null);
                Password = (string)Registry.GetValue(KeyPath, PasswordValueName, null);

                if (UserName == null || Password == null)
                    return false;

                return true;
            }
            catch(Exception ex )
            {
                System.Windows.Forms.MessageBox.Show($"An error ocurred : {ex.Message}", "Error!",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
