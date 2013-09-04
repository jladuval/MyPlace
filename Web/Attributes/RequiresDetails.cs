namespace Web.Attributes
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Web.Core.Extensions;

    public class RequiresDetailsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var user = HttpContext.Current.User.TryGetPrincipal();
            if (user != null)
            {
                if (!user.HasDetails)
                {
                    actionContext.Result =
                        new RedirectToRouteResult(
                            new RouteValueDictionary
                                {
                                    { "controller", "Membership" }, 
                                    { "action", "MoreDetails" }
                                });
                }
            }
            base.OnActionExecuting(actionContext);
        }
    }
}