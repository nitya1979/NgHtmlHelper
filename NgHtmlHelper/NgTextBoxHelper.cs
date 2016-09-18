using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Capgemini.MVC.NgHtmlHelper
{
    public static class NgTextBoxHelper
    {
        public static MvcHtmlString NgTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.NgTextBoxFor(expression, null);
        }

        public static MvcHtmlString NgTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
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
                        validationSpans.Add(NgHtmlHelper.GetSpan(nameArg.TypedValue.Value.ToString(), member.Name.ToLower(), inputType));
                    }
                }
                else if (cs.AttributeType == typeof(RequiredAttribute))
                {
                    tagBuilder.MergeAttribute("required", string.Empty);

                    validationSpans.Add(NgHtmlHelper.GetRequiredSpan(cs, member));
                    
                }
                else if (cs.AttributeType == typeof(MaxLengthAttribute))
                {
                    tagBuilder.MergeAttribute("maxlength", cs.ConstructorArguments[0].Value.ToString());
                }

            }

            if (member.CustomAttributes.Where(attr => attr.AttributeType == typeof(DataTypeAttribute)).Count() == 0)
            {
                tagBuilder.MergeAttribute("type", "text");
                tagBuilder.MergeAttribute("name", member.Name.ToLower());
            }

            if (htmlAttributes != null)
            {
                RouteValueDictionary htmlAttr = new RouteValueDictionary(htmlAttributes);

                foreach (string key in htmlAttr.Keys)
                {
                    tagBuilder.MergeAttribute(key, htmlAttr[key].ToString());
                }
            }

            tagBuilder.MergeAttribute("ng-model", member.ReflectedType.Name.ToCamelCase() +"."+ member.Name);

            string finalHtml = tagBuilder.ToString(TagRenderMode.SelfClosing);

            foreach (TagBuilder span in validationSpans)
            {
                finalHtml += span.ToString(TagRenderMode.Normal);
            }
            return MvcHtmlString.Create(finalHtml);

        }
    }
}
