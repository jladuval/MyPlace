using System.Web;

namespace CQRS.Security.Interfaces.Application.Impersonation
{
    public interface IIdentityProvider
    {
        void ProcessIdentity(HttpApplication application);
    }
}
