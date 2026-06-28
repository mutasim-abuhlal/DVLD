using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DVLD_DataAccess
{
    internal static class clsDataAccessSettings
    { 
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DVLD_ConnectionString"].ConnectionString;

        public static string AppName = ConfigurationManager.AppSettings["AppName"];
    }
}
