using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Capgemini.MVC.NgHtmlHelper
{
    public static class NgRadioButtonHelper
    {
        public static MvcHtmlString NgRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value, string displayText, object htmlAttributes)
        {
            MemberInfo member = ((MemberExpression)expression.Body).Member;
            var tagBuilder = new TagBuilder("input");

            RouteValueDictionary htmlAttr = new RouteValueDictionary(htmlAttributes);
            bool inline = false;

            
            if (htmlAttr.Keys.Any(k => k.ToLower().Equals("class")) && htmlAttr["class"].ToString().Contains("inline"))
                inline = true;

            tagBuilder.MergeAttribute("type", "radio");
            tagBuilder.MergeAttribute("name", member.Name.ToLower());
            tagBuilder.MergeAttribute("id", member.Name.ToLower());
            tagBuilder.MergeAttribute("ng-model", member.ReflectedType.Name.ToCamelCase() + "." + member.Name);
            tagBuilder.MergeAttribute("value", value.ToString());

            
            foreach (string key in htmlAttr.Keys)
            {
                if( !key.ToLower().Equals("class"))
                {
                    tagBuilder.MergeAttribute(key, htmlAttr[key].ToString());
                }
            }

            string finalHtml = string.Empty;

            if (inline)
            {
                finalHtml = "<label class=\"radio-inline\">" + tagBuilder.ToString(TagRenderMode.SelfClosing) + " " +
                               displayText + "</label>";
            }
            else
            {
                TagBuilder divTag = new TagBuilder("div");
                divTag.MergeAttribute("class", "radio");
                divTag.InnerHtml = "<label>" + tagBuilder.ToString(TagRenderMode.SelfClosing) + " " + displayText + "</label>";
                finalHtml = divTag.ToString(TagRenderMode.Normal);
            }

            return MvcHtmlString.Create(finalHtml);
        }

        public static MvcHtmlString NgRadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
                                                                            string ngList, string valueField, string textField, object htmlAttributes)
        {
            RouteValueDictionary htmlAttr = new RouteValueDictionary(htmlAttributes);
            //TagBuilder mainDivTag = new TagBuilder("div");
            //mainDivTag.MergeAttribute("ng-repeat", "item in " + ngList);

            bool inline = false;

            if (htmlAttr.Keys.Any(k => k.ToLower().Equals("class")) && htmlAttr["class"].ToString().Contains("inline"))
                inline = true;

            
            
            MemberInfo member = ((MemberExpression)expression.Body).Member;
            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", "radio");
            tagBuilder.MergeAttribute("name", member.Name.ToLower());
            tagBuilder.MergeAttribute("id", member.Name.ToLower());
            tagBuilder.MergeAttribute("ng-model", member.ReflectedType.Name.ToCamelCase() + "." + member.Name);
            tagBuilder.MergeAttribute("ng-value", "item." + valueField);

            string finalHtml = string.Empty;

            if(inline)
            {
                finalHtml = "<label class=\"radio-inline\" ng-repeat=\"item in "+ngList+"\">" + tagBuilder.ToString(TagRenderMode.SelfClosing) + " " +
                               "{{item." + textField + "}}</label>";
            }
            else
            {
                TagBuilder divTag = new TagBuilder("div");
                if (htmlAttr.Keys.Any(k => k.ToLower().Equals("class")))
                    divTag.MergeAttribute("class", htmlAttr["class"].ToString());
                divTag.MergeAttribute("ng-repeat", "item in " + ngList);
                divTag.InnerHtml = "<label>" + tagBuilder.ToString(TagRenderMode.SelfClosing) + "{{item." + textField + "}}</label>";
                finalHtml = divTag.ToString(TagRenderMode.Normal); ;
            }
            
            //mainDivTag.InnerHtml = divTag.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(finalHtml);
        }
    }
}
