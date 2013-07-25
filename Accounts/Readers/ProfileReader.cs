namespace Accounts.Readers
{
    using System;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using Base.CQRS.Query.Attributes;
    using Domain;
    using Interfaces.Readers;
    using NHibernate;
    using NHibernate.Linq;

    [Reader]
    public class ProfileReader : IProfileReader
    {
        private readonly ISession _session;

        public ProfileReader(ISession session)
        {
            _session = session;
        }

        public string GetImageUrl(Guid userId, string fileName)
        {
            return
                _session.Query<User>()
                    .Fetch(x => x.ProfileImages)
                    .Single(x => x.Id == userId)
                    .ProfileImages.Single(x => x.ImageName == fileName)
                    .Url;
        }
    }
}
