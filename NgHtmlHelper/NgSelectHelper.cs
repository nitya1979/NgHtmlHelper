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
    public static class NgSelectHelper
    {
        public static MvcHtmlString NgComboBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string options, string valueField, string textField, object htmlAttributes)
        {
            MemberInfo member = ((MemberExpression)expression.Body).Member;

            var tagBuilder = new TagBuilder("select");
            List<TagBuilder> validationSpans = new List<TagBuilder>();

            tagBuilder.MergeAttribute("name", member.Name.ToLower());

            foreach (CustomAttributeData cs in member.CustomAttributes)
            {
                if (cs.AttributeType == typeof(RequiredAttribute))
                {
                    tagBuilder.MergeAttribute("required", string.Empty);

                    validationSpans.Add(NgHtmlHelper.GetRequiredSpan(cs, member));
                }
            }

            RouteValueDictionary htmlAttr = new RouteValueDictionary(htmlAttributes);

            foreach (string key in htmlAttr.Keys)
            {
                tagBuilder.MergeAttribute(key, htmlAttr[key].ToString());
            }

            tagBuilder.MergeAttribute("ng-model", member.ReflectedType.Name.ToCamelCase() + "." + member.Name);

            TagBuilder optionBuilder = new TagBuilder("option");
            optionBuilder.MergeAttribute("ng-repeat", "opt in " + options);
            optionBuilder.MergeAttribute("value", "{{opt."+valueField +"}}");
            optionBuilder.InnerHtml = "{{opt." + textField + "}}";

            tagBuilder.InnerHtml = optionBuilder.ToString();

            string finalHtml = tagBuilder.ToString(TagRenderMode.Normal);

            foreach (TagBuilder span in validationSpans)
            {
                finalHtml += span.ToString(TagRenderMode.Normal);
            }

            return MvcHtmlString.Create(finalHtml);

        }
    }
}
