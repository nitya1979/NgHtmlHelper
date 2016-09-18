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
    public static class NgCheckBoxHelper
    {
        public static MvcHtmlString NgCheckBoxFor<TModel, TProperty>( this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, 
                                                                        object htmlAttributes)
        {
            return htmlHelper.NgCheckBoxFor(expression, null, null, htmlAttributes);

        }

        public static MvcHtmlString NgCheckBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
                                                                     string trueValue, string falseValue, object htmlAttributes)
        {
            MemberInfo member = ((MemberExpression)expression.Body).Member;

            var tagBuilder = GetCheckBox(member, trueValue, falseValue);

            bool inline = false;

            RouteValueDictionary htmlAttr = null;

            if( htmlAttributes != null)
            {
                htmlAttr = new RouteValueDictionary(htmlAttributes);
                
                if(htmlAttr.ContainsKey("class"))
                {
                    if (htmlAttr["class"].ToString().Contains("inline"))
                        inline = true;

                    htmlAttr.Remove("class");
                }

                foreach (string key in htmlAttr.Keys)
                {
                    tagBuilder.MergeAttribute(key, htmlAttr[key].ToString());
                }
            }
            string displayText = member.Name;
            foreach (CustomAttributeData cs in member.CustomAttributes)
            {
                if (cs.AttributeType == typeof(DisplayAttribute))
                {
                    CustomAttributeNamedArgument nameArg = cs.NamedArguments.Where(m => m.MemberName == "Name").First();
                    if (nameArg == null || string.IsNullOrEmpty(nameArg.TypedValue.Value.ToString()))
                        displayText = member.Name;
                    else
                        displayText = nameArg.TypedValue.Value.ToString();
                }
            }

            string finalHtml = string.Empty;
            if(inline)
            {
                finalHtml = "<label class=\"checkbox-inline\">" + tagBuilder.ToString(TagRenderMode.SelfClosing) + " " +
                               displayText + "</label>"; 
            }
            else
            {
                TagBuilder divTag = new TagBuilder("div");
                divTag.AddCssClass("checkbox");
                divTag.InnerHtml = "<label>" + tagBuilder.ToString(TagRenderMode.SelfClosing) + " "+displayText+"</label>";
                finalHtml = divTag.ToString(TagRenderMode.Normal); ;
            }
            return MvcHtmlString.Create(finalHtml);

        }

        public static MvcHtmlString NgCheckBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.NgCheckBoxFor(expression, null);
        }

        public static MvcHtmlString NgCheckBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
                                                                        string trueValue, string falseValue)
        {
            return htmlHelper.NgCheckBoxFor(expression, trueValue, falseValue, null);
        }

        private static TagBuilder GetCheckBox(MemberInfo member, string trueValue = null, string falseValue = null)
        {
            var checkBox = new TagBuilder("input");
            checkBox.MergeAttribute("type", "checkbox");
            checkBox.MergeAttribute("name", member.Name.ToLower());
            checkBox.MergeAttribute("id", member.Name.ToLower());
            checkBox.MergeAttribute("ng-model", member.ReflectedType.Name.ToCamelCase() + "." + member.Name);

            if (!string.IsNullOrEmpty(trueValue))
                checkBox.MergeAttribute("ng-true-value", "'"+trueValue+"'");

            if(!string.IsNullOrEmpty(falseValue))
                checkBox.MergeAttribute("ng-false-value", "'" + falseValue + "'");

            return checkBox;

        }
    }
}
