using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilityLib;

namespace DVLD.Global
{
    internal static class clsUtil
    {
        private static string GenerateGUID()
        {
            return Guid.NewGuid().ToString();
        }

        private static string ReturnGUIDImagePathWithImageExtension(string SourceImagePath)
        {
            FileInfo fi = new FileInfo(SourceImagePath);
            return GenerateGUID() + fi.Extension;
        }

        private static bool _CreateFolderImageIfItIsntExist(string FolderImgaesPath)
        {
            try
            {
                if(!Directory.Exists(FolderImgaesPath))
                {
                    Directory.CreateDirectory(FolderImgaesPath);
                    return true;
                }
            }
            catch(Exception ex)
            {
                clsUtility.WriteEventLogEntry(clsGlobal.AppName, ex.Message, System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show(ex.Message.ToString(), "Create Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public static bool CopyImageToFolderImages(ref string SourceImagePath)
        {
            string FolderImagePath = @"C:\DVLD-People-Images";

            if (!_CreateFolderImageIfItIsntExist(FolderImagePath))
                return false;

            string NewImagePath = Path.Combine(FolderImagePath, ReturnGUIDImagePathWithImageExtension(SourceImagePath));

            try
            {
                File.Copy(SourceImagePath, NewImagePath);
            }
            catch(Exception ex)
            {
                clsUtility.WriteEventLogEntry(clsGlobal.AppName, ex.Message, System.Diagnostics.EventLogEntryType.Error);
                return false;
            }
            SourceImagePath = NewImagePath;
            return true;
        }

    }
}
