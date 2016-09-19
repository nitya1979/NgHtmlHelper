using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Capgemini.MVC.NgHtmlHelper
{
    public static class NgDateBoxHelper 
    {
        public static MvcHtmlString NgDateBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, 
           object htmlAttributes, string icon, string onClick, string format, string strIsOpen, string dateOptions)
        {
            MemberInfo member = ((MemberExpression)expression.Body).Member;
            
            var tagBuilder = new TagBuilder("input");

            List<TagBuilder> validationSpans = new List<TagBuilder>();

            foreach (CustomAttributeData cs in member.CustomAttributes)
            {
                if (cs.AttributeType == typeof(DataTypeAttribute))
                {
                    string inputType = HelperConstants.DataTypeMapping[(DataType)cs.ConstructorArguments[0].Value];
                    tagBuilder.MergeAttribute("type", inputType);
                    tagBuilder.MergeAttribute("name", member.Name.ToLower());

                    CustomAttributeNamedArgument nameArg = cs.NamedArguments.Where(m => m.MemberName == "ErrorMessage").First();

                    if (nameArg != null)
                    {
                        validationSpans.Add(NgHtmlHelper.GetSpan(nameArg.TypedValue.Value.ToString(), member.Name.ToLower(), "date"));
                    }
                }
                else if (cs.AttributeType == typeof(RequiredAttribute))
                {
                    tagBuilder.MergeAttribute("ng-required", "true");

                    validationSpans.Add(NgHtmlHelper.GetRequiredSpan(cs, member));
                }

            }

            if (member.CustomAttributes.Where(attr => attr.AttributeType == typeof(DataTypeAttribute)).Count() == 0)
            {
                tagBuilder.MergeAttribute("type", "text");
                tagBuilder.MergeAttribute("name", member.Name.ToLower());
            }
            tagBuilder.MergeAttribute("readonly", "readonly");
            RouteValueDictionary htmlAttr = new RouteValueDictionary(htmlAttributes);

            NgHtmlHelper.SetHtmlAttributes(tagBuilder, htmlAttributes);

            tagBuilder.MergeAttribute("ng-model", member.ReflectedType.Name.ToCamelCase() + "." + member.Name);

            if (string.IsNullOrEmpty(format)) format = "MM/dd/yyyy";
            tagBuilder.MergeAttribute("uib-datepicker-popup", format);

            tagBuilder.MergeAttribute("is-open", strIsOpen);

            tagBuilder.MergeAttribute("close-text", "Close");

            if (!string.IsNullOrEmpty(dateOptions))
            {
                tagBuilder.MergeAttribute("datepicker-options", dateOptions);
            }

            string finalHtml = string.Empty;

            if (!string.IsNullOrEmpty(icon))
            {
                var tagDiv = new TagBuilder("div");
                tagDiv.AddCssClass("input-group");
                tagDiv.MergeAttribute("id", member.Name.ToLower());

                var tagIconDiv = new TagBuilder("span");

                tagIconDiv.AddCssClass("input-group-btn");

                StringBuilder strBtnSpan = new StringBuilder();
                strBtnSpan.AppendFormat("<button type =\"button\" class=\"btn btn-default\" ng-click=\"{0}()\" ><i class=\"{1}\"></i></button>", onClick, icon);

                tagIconDiv.InnerHtml = strBtnSpan.ToString();

                tagDiv.InnerHtml = tagBuilder.ToString(TagRenderMode.SelfClosing) +
                                   tagIconDiv.ToString(TagRenderMode.Normal);

                finalHtml = tagDiv.ToString(TagRenderMode.Normal);
            }
            else
            {
                tagBuilder.MergeAttribute("id", member.Name.ToLower());
                finalHtml = tagBuilder.ToString(TagRenderMode.SelfClosing);
            }
            
           // tagBuilder        

            foreach (TagBuilder span in validationSpans)
            {
                finalHtml += span.ToString(TagRenderMode.Normal);
            }
            return MvcHtmlString.Create(finalHtml);

        }
    }
}
