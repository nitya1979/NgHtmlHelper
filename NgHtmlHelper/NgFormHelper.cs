using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Capgemini.MVC.NgHtmlHelper
{
    public static class NgFormHelper
    {
        private class FormWriter : IDisposable
        {
            TextWriter writer;

            public FormWriter(TextWriter writer)
            {
                this.writer = writer;
            }

            public void Dispose()
            {
                this.writer.Write("</form>");
            }
        }

        public static IDisposable NgBeginForm(this HtmlHelper htmlHelper, string cssClass)
        {
            var writer = htmlHelper.ViewContext.Writer;

            writer.Write(string.Format("<form name=\"myForm\" class=\"{0}\" novalidate>", cssClass));

            return new FormWriter(writer);
        }
    }
}
