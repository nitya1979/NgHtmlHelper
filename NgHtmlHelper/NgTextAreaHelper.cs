using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace Capgemini.MVC.NgHtmlHelper
{
    public static class NgTextAreaHelper
    {

        public static MvcHtmlString NgTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            MemberInfo member = ((MemberExpression)expression.Body).Member;

            var tagBuilder = new TagBuilder("textarea");

            tagBuilder.MergeAttribute("name", member.Name.ToLower());

            RouteValueDictionary htmlAttr = new RouteValueDictionary(htmlAttributes);

            foreach (string key in htmlAttr.Keys)
            {
                tagBuilder.MergeAttribute(key, htmlAttr[key].ToString());
            }

            List<TagBuilder> validationSpans = new List<TagBuilder>();

            foreach (CustomAttributeData cs in member.CustomAttributes)
            {
                if (cs.AttributeType == typeof(RequiredAttribute))
                {
                    tagBuilder.MergeAttribute("Required", string.Empty);

                    validationSpans.Add(NgHtmlHelper.GetRequiredSpan(cs, member));
                }
                else if (cs.AttributeType == typeof(MaxLengthAttribute))
                {
                    tagBuilder.MergeAttribute("maxlength", cs.ConstructorArguments[0].Value.ToString());
                }
            }

            tagBuilder.MergeAttribute("ng-model", member.ReflectedType.Name.ToCamelCase() + "." + member.Name);

            string finalHtml = tagBuilder.ToString(TagRenderMode.Normal);

            foreach (TagBuilder span in validationSpans)
            {
                finalHtml += span.ToString(TagRenderMode.Normal);
            }

            return MvcHtmlString.Create(finalHtml);

        }
    }
}
