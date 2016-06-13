using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Capgemini.MVC.NgHtmlHelper
{
    class NgHtmlHelper
    {
        public static TagBuilder GetSpan(string message, string fieldName, string validation)
        {
            var span = new TagBuilder("span");

            span.MergeAttribute("ng-show", "myForm." + fieldName + ".$error." + validation);

            span.AddCssClass("text-danger");

            span.InnerHtml = message.Trim('"');

            return span;
        }

        public static TagBuilder GetRequiredSpan(CustomAttributeData cs, MemberInfo member)
        {
            CustomAttributeNamedArgument namedArg = cs.NamedArguments.Where(p => p.MemberName == "ErrorMessage").First();
            string errorMsg = string.Empty;

            if (namedArg != null)
                errorMsg = namedArg.TypedValue.Value.ToString();
            else
                errorMsg = "Please enter " + member.Name.ToLower();

            return GetSpan(errorMsg, member.Name.ToLower(), "required");
        }
    }
}
