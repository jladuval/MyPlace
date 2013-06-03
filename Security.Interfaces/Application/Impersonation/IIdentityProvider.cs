using System.Web;

namespace Security.Interfaces.Application.Impersonation
{
    public interface IIdentityProvider
    {
        void ProcessIdentity(HttpApplication application);
    }
}
