namespace Web.Extensions
{
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class HtmlExtensions
    {

        public static MvcHtmlString MenuItem(this HtmlHelper helper, string linkText, string actionName, string controllerName, string localHref = null, bool localOnly = false)
        {
            var currentAction = helper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = helper.ViewContext.RouteData.GetRequiredString("controller");
            var className = string.Empty;
            var active = actionName == currentAction && controllerName == currentController;
            if(!active && localOnly)
                return new MvcHtmlString(String.Empty);

            if (active && !localOnly)
                className = "active";

            string anchor;
            if (!active || localHref == null)
                anchor = helper.ActionLink(
                    linkText,
                    actionName,
                    controllerName).ToHtmlString();
            else
                anchor = String.Format("<a href={0}>{1}</a>", localHref, linkText);
            return new MvcHtmlString(string.Format("<li class='{0}'>{1}</li>", className, anchor));
        }
    }
}