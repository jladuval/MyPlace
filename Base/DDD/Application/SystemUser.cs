using Base.Infrastructure.Attributes;

namespace Base.DDD.Application
{
    [Component]
    public class SystemUser : ISystemUser
    {
        public int UserId
        {
            get { return 1; }
        }
    }
}
