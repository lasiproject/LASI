using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LASI.Core;
using LASI.Utilities;
using Microsoft.AspNet.Mvc.Rendering;

namespace AspSixApp.WebHelpers
{
    public static class BootstrapContextmenuHtmlHelperExtensions
    {
        public static HtmlString BootstrapContextMenuItemFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression) where TModel : class, ILexical where TValue : IEnumerable<ILexical>
        {
            var value = expression.Compile()(html.ViewData.Model);
            var builder = new StringBuilder($"<ul class='dropdown-menu' role='menu' aria-labelledby='dropdownMenu'>");
            value.ToList().ForEach(e =>
            {
                var cssClass = e.Match();
                builder.Append($"<li class='{cssClass}'><a href='#'> View Referenced (@data.RefersTo)</a></li>");
            });
            builder.Append("</ul>");
            return new HtmlString(builder.ToString());
        }
    }
}
