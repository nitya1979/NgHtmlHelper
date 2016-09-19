using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

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

        public static void SetHtmlAttributes(TagBuilder tagBuilder,  object htmlAttributes)
        {
            if (htmlAttributes != null)
            {
                RouteValueDictionary htmlAttr = new RouteValueDictionary(htmlAttributes);

                foreach (string key in htmlAttr.Keys)
                {
                    if (key.StartsWith("ng"))
                        tagBuilder.MergeAttribute("ng-" + key.Substring(2), htmlAttr[key].ToString());
                    else
                        tagBuilder.MergeAttribute(key, htmlAttr[key].ToString());
                }
            }

        }
    }
}
