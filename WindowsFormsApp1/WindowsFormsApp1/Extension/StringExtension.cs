using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Extension
{
    public static class StringExtension
    {
        public static string UnitAmount(this string unitType)
        {
            return string.IsNullOrEmpty(unitType) ? "0" : unitType;
        }
    }
}
