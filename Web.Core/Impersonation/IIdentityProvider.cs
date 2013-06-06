namespace Web.Core.Impersonation
{
    using System.Web;

    public interface IIdentityProvider
    {
        void ProcessIdentity(HttpApplication application);
    }
}
