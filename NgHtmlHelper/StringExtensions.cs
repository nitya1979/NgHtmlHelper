using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.MVC.NgHtmlHelper
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            return str.Substring(0, 1).ToLowerInvariant() + str.Substring(1, str.Length - 1);
        }
    }
}
