using System;
using System.Security.Principal;
using System.Web;

namespace CQRS.Security.Interfaces.Application.Extensions
{
    public static class UserPrincipalExtensions
    {
        public static ICustomPrincipal TryGetPrincipal(this IPrincipal user)
        {
            return user as ICustomPrincipal;
        }

        public static ICustomPrincipal GetPrincipal(this IPrincipal user)
        {
            CheckUser(user);
            return (ICustomPrincipal)user;
        }

        public static ICustomPrincipal TryGetUser(this HttpContextBase context)
        {
            return TryGetPrincipal(context.User);
        }

        public static ICustomPrincipal TryGetUser(this HttpContext context)
        {
            return TryGetPrincipal(context.User);
        }

        public static ICustomPrincipal GetUser(this HttpContextBase context)
        {
            return GetPrincipal(context.User);
        }

        public static ICustomPrincipal GetUser(this HttpContext context)
        {
            return GetPrincipal(context.User);
        }

        public static bool IsAuthenticated(this IPrincipal user)
        {
            return user.Identity.IsAuthenticated;
        }

        private static void CheckUser(IPrincipal user)
        {
            if (!user.IsAuthenticated())
                throw new InvalidOperationException("Cannot perform the operation due to the user is not authenticated.");
        }
    }
}
