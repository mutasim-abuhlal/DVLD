using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVLD.Global
{
    internal static class clsValidation
    {
        public static bool IsValidEmail(string Email)
        {
            string Pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(Pattern);

            return regex.IsMatch(Email);
        }
        public static bool IsFloat(string Number)
        {
            return float.TryParse(Number, out float _);
        }
        public static bool IsIntger(string Number)
        {
            return Int32.TryParse(Number, out int _);
        }
        public static bool IsNumber(string Number)
        {
            return IsIntger(Number) || IsFloat(Number);   
        }
    }
}
