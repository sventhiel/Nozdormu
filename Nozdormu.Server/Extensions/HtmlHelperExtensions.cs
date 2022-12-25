using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nozdormu.Server.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent HelloWorldHTMLString(this IHtmlHelper htmlHelper)
        {
            return new HtmlString("<button onclick=\"deletethething(1)\">Test</button>");
        }
    }
}
