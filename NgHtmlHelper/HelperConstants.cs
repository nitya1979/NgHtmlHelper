using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.MVC.NgHtmlHelper
{
    internal static class HelperConstants
    {
        internal static Dictionary<DataType, string> DataTypeMapping = new Dictionary<DataType, string>();
        
        static HelperConstants()
        {
            DataTypeMapping.Add(DataType.EmailAddress, "email");
            DataTypeMapping.Add(DataType.Password, "password");
            DataTypeMapping.Add(DataType.Text, "text");
            DataTypeMapping.Add(DataType.MultilineText, "textarea");
            DataTypeMapping.Add(DataType.Date, "text");
            DataTypeMapping.Add(DataType.DateTime, "text");
        }
    }
}
