namespace Accounts.Readers.Queries.Dinner
{
    using System;
    using System.Linq;
    using Domain;
    using Interfaces.Presentation.Dinner;
    using NHibernate;
    using NHibernate.Linq;

    public class GetDinnerForReviewQuery
    {
        private readonly ISession _session;

        public GetDinnerForReviewQuery(ISession session)
        {
            _session = session;
        }

        public ReviewApplicantsDto Execute(Guid dinnerId)
        {
            var dinner = _session.Query<Dinner>()
                .FetchMany(x => x.Applicants)
                .ThenFetch(x => x.User)
                .FetchMany(x => x.Applicants)
                .ThenFetch(x => x.Partner)
                .Single(x => x.Id == dinnerId);
            return new ReviewApplicantsDto
            {
                Id = dinner.Id,
                Date = dinner.Date,
                Dessert = dinner.Dessert,
                Main = dinner.Main,
                Starter = dinner.Starter,
                Applicants = dinner.Applicants
                        .Where(y => !y.Hidden)
                        .Select(y => new DinnerApplicantDto
                        {
                            Id = y.Id,
                            Name = y.User.FullName(),
                            ApplicantId = y.User.Id,
                            GenderOrientation =
                                String.Format("{0} {1}", y.User.Gender.ToString(), y.User.Orientation.ToString()),
                            PartnerGenderOrientation = y.Partner == null ? null :
                                String.Format("{0} {1}", y.Partner.Gender.ToString(), y.Partner.Orientation.ToString()),
                            PartnerId = y.Partner == null ? null : (Guid?)y.Partner.Id,
                            PartnerName = y.Partner == null ? null : y.Partner.FullName()
                        }).ToList()
            };
        }
    }
}
