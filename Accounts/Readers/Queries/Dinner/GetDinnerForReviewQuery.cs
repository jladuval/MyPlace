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
            return _session.Query<Dinner>()
                .FetchMany(x => x.Applicants)
                .ThenFetch(x => x.User)
                .FetchMany(x => x.Applicants)
                .ThenFetch(x => x.Partner)
                .Where(x => x.Id == dinnerId)
                .Select(x => new ReviewApplicantsDto
                {
                    Id = x.Id,
                    Date = x.Date.ToShortDateString(),
                    Dessert = x.Dessert,
                    Main = x.Main,
                    Starter = x.Starter,
                    Applicants = x.Applicants
                        .Where(y => !y.Hidden)
                        .Select(y => new DinnerApplicantDto
                        {
                            Id = y.Id,
                            Name = x.User.FullName(),
                            ApplicantId = y.Id,
                            GenderOrientation =
                                String.Format("{0} {1}", y.User.Gender.ToString(), y.User.Orientation.ToString()),
                            PartnerGenderOrientation =
                                String.Format("{0} {1}", y.Partner.Gender.ToString(), y.Partner.Orientation.ToString()),
                            PartnerId = y.Partner.Id,
                            PartnerName = y.Partner.FullName()
                        }).ToList()
                }).Single();
        }
    }
}
