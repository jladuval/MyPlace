namespace Accounts.Readers.Queries.Dinner
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Domain;
    using Interfaces.Presentation.Dinner;
    using NHibernate;
    using NHibernate.Engine;
    using NHibernate.Linq;

    internal class GetDinnersAsHostQuery
    {
        readonly Expression<Func<Dinner, bool>> _pastQuery = x => x.Date < DateTime.UtcNow;

        readonly Expression<Func<Dinner, bool>> _currentQuery = x => x.Date > DateTime.UtcNow;

        private readonly ISession _session;

        public GetDinnersAsHostQuery(ISession session)
        {
            _session = session;
        }

        public ICollection<PersonalDinnerListItem> Execute(Guid userId, bool getPast)
        {
            var timeFilter = getPast ? _pastQuery : _currentQuery;

            var asHost = _session.Query<Dinner>()
                .Where(x => x.User.Id == userId)
                .Where(timeFilter)
                .Where(x => x.User.Id == userId || x.Partner.Id == userId)
                .Select(x => new PersonalDinnerListItem
                {
                    ApplicantCount = x.Applicants.Count,
                    Pending = x.VerificationCode != null,
                    ApprovalToken = x.User.Id == userId ? null : x.VerificationCode,
                    Host = "You",
                    DinnerId = x.Id,
                    Partner = DinnerHostPartner(x, userId),
                    Date = x.Date.ToShortDateString()
                }).ToList();

            return asHost;
        }

        private string DinnerHostPartner(Dinner dinner, Guid userId)
        {
            if (dinner.Partner == null)
                return null;
            return dinner.Partner.Id == userId
                ? dinner.User.FullName()
                : dinner.Partner.FullName();
        }
    }
}
