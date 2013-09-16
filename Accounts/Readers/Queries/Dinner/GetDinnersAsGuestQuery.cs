namespace Accounts.Readers.Queries.Dinner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Domain;
    using Interfaces.Presentation.Dinner;
    using NHibernate;
    using NHibernate.Linq;

    internal class GetDinnersAsGuestQuery
    {
        readonly Expression<Func<DinnerApplicant, bool>> _pastQuery = x => x.Accepted && x.Dinner.Date < DateTime.UtcNow;

        readonly Expression<Func<DinnerApplicant, bool>> _currentQuery = x => x.Dinner.Date > DateTime.UtcNow;

        private readonly ISession _session;

        public GetDinnersAsGuestQuery(ISession session)
        {
            _session = session;
        }

        public ICollection<PersonalDinnerListItem> Execute(Guid userId, bool getPast)
        {
            var timeFilter = getPast ? _pastQuery : _currentQuery;
            var result = _session.Query<DinnerApplicant>()
               .Fetch(x => x.Dinner).ThenFetch(x => x.User)
               .Fetch(x => x.Dinner).ThenFetch(x => x.Partner)
               .Where(timeFilter)
               .Where(x => x.User.Id == userId || x.Partner.Id == userId)
               .Select(x => new PersonalDinnerListItem
               {
                   Accepted = x.Accepted,
                   DinnerId = x.Dinner.Id,
                   ApplicantCount = x.Dinner.Applicants.Count,
                   AppliedPartner = AppliedPartner(x, userId),
                   ApprovalToken = x.User.Id == userId ? null : x.VerificationCode,
                   Date = x.Dinner.Date.ToShortDateString(),
                   Host = x.Dinner.User.FullName(),
                   Partner = x.Dinner.Partner == null ? null : x.Dinner.Partner.FullName(),
                   Pending = x.VerificationCode != null
               }).ToList();

            return result.ToList();
        }

        private string AppliedPartner(DinnerApplicant dinnerApplicant, Guid userId)
        {
            if (dinnerApplicant.Partner == null)
                return null;
            return dinnerApplicant.Partner.Id == userId
                ? dinnerApplicant.User.FullName()
                : dinnerApplicant.Partner.FullName();
        }
    }
}
