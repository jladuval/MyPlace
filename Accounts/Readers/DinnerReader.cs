namespace Accounts.Readers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Accounts.Domain;
    using AutoMapper;
    using Base.CQRS.Query.Attributes;
    using Interfaces.Presentation.Comments;
    using Interfaces.Presentation.Dinner;
    using Interfaces.Presentation.Profile;
    using Interfaces.Readers;
    using NHibernate;
    using NHibernate.Linq;
    using NHibernate.Transform;

    [Reader]
    public class DinnerReader : IDinnerReader
    {
        private readonly ISession _session;

        public DinnerReader(ISession session)
        {
            _session = session;
        }

        public DinnerListDto GetDinnerList(double lat, double lng, int skip, int take)
        {
            var selectDinnerByDistance = string.Format(
                @"DECLARE @dist AS Geography = 'POINT({0} {1})'
                Select 
                    u.ProfileImageUrl as ProfileImageUrl, 
                    d.Starter as Starter,
                    d.Main as Main,
                    d.Id as Id,
                    d.Dessert as Dessert,
                    d.Dry as DryDinner,
                    d.[Date] as EventDate,
                    l.GeoLoc.STDistance(@dist.STBuffer(0.2).STAsText()) as Distance
                    from dbo.Locations l
                    join Dinners d on d.LocationId = l.Id
                    join Users u on u.Id = d.UserId
                    Order by Distance asc
                    OFFSET {2} ROWS
                    FETCH NEXT {3} ROWS ONLY", 
                lng,
                lat, 
                skip, 
                take);

            var dinnerListByDistance = _session.CreateSQLQuery(selectDinnerByDistance)
                .SetResultTransformer(Transformers.AliasToBean<DinnerListItemDto>())
                .List<DinnerListItemDto>();

            var total = _session.Query<Dinner>().Count(x => x.Date < DateTime.UtcNow);

            return new DinnerListDto
                       {
                           TotalPages = total / take,
                           TotalResults = total,
                           Dinners = dinnerListByDistance
                       };
        }

        public DinnerDto GetDinner(Guid id, Guid userId)
        {
            var dinner = _session.Query<Dinner>().Fetch(x => x.User).Fetch(x => x.Partner).Single(x => x.Id == id);
            Mapper.CreateMap<Dinner, DinnerDto>()
                .ForMember(x => x.ProfileImageUrl, opt => opt.MapFrom(x => x.User.ProfileImageUrl))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.User.Id))
                .ForMember(x => x.HasApplied, opt => opt.MapFrom(x => HasApplied(x.Applicants, userId)));
            var dto = Mapper.Map<DinnerDto>(dinner);
            
            if (dinner.Partner != null)
            {
                var partner = dinner.Partner;
                dto.PartnerFirstName = partner.FirstName;
                dto.PartnerLastName = partner.LastName;
                dto.PartnerId = partner.Id;
                dto.PartnerImageUrl = partner.ProfileImageUrl;
            }
            return dto;
        }

        public DinnerConfirmDto DinnerCanBeConfirmedByPartner(string token)
        {
            DinnerConfirmDto result = null;
            Guid verificationToken;
            if (Guid.TryParse(token, out verificationToken))
            {
                var dinner = _session.Query<Dinner>().Fetch(x => x.Partner).FirstOrDefault(x => x.VerificationCode == verificationToken);
                if (dinner != null)
                {
                    result = new DinnerConfirmDto
                        {
                            Id = dinner.Id,
                            UserId = dinner.Partner.Id
                        };
                }
            }
            return result;
        }

        public DinnerConfirmDto InvitationCanBeConfirmedByPartner(string token)
        {
            DinnerConfirmDto result = null;
            Guid verificationToken;
            if (Guid.TryParse(token, out verificationToken))
            {
                var invitation =
                    _session.Query<DinnerApplicant>()
                        .Fetch(x => x.Dinner)
                        .FirstOrDefault(x => x.VerificationCode == verificationToken);
                if (invitation != null)
                {
                    result = new DinnerConfirmDto
                    {
                        Id = invitation.Dinner.Id,
                        UserId = invitation.Partner.Id
                    };
                }
            }
            return result;
        }

        public ICollection<CommentDto> GetCommentsForDinner(Guid dinnerId)
        {
            return _session.Query<Dinner>()
                .FetchMany(x => x.Comments)
                .ThenFetch(x => x.User)
                .Single(x => x.Id == dinnerId)
                .Comments.Select(x => new CommentDto
                {
                    UserId = x.User.Id.ToString(),
                    Id = x.Id.ToString(),
                    ProfileImageUrl = x.User.ProfileImageUrl,
                    CreatedDate = x.CreatedDate,
                    Name = x.User.FullName(),
                    Text = x.Text
                }).ToList();
        }

        public ICollection<PersonalDinnerListItem> GetAppliedDinnerList(Guid userId)
        {
            return _session.Query<DinnerApplicant>()
                .Fetch(x => x.Dinner).ThenFetch(x => x.User)
                .Fetch(x => x.Dinner).ThenFetch(x => x.Partner)
                .Where(x => x.User.Id == userId && !x.Hidden)
                .Where(x => x.Dinner.Date > DateTime.UtcNow)
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
        }

        public ICollection<PersonalDinnerListItem> GetAttendedDinnerList(Guid userId)
        {
            var asGuest = _session.Query<DinnerApplicant>()
               .Fetch(x => x.Dinner).ThenFetch(x => x.User)
               .Fetch(x => x.Dinner).ThenFetch(x => x.Partner)
               .Where(x => x.Dinner.Date < DateTime.UtcNow)
               .Where(x => x.Accepted)
               .Select(x => new PersonalDinnerListItem
               {
                   DinnerId = x.Dinner.Id,
                   AppliedPartner = AppliedPartner(x, userId),
                   Date = x.Dinner.Date.ToShortDateString(),
                   Host = x.Dinner.User.FullName(),
                   Partner = x.Dinner.Partner == null ? null : x.Dinner.Partner.FullName(),
               }).ToList();

            var asHost = _session.Query<Dinner>()
                .Where(x => x.User.Id == userId)
                .Where(x => x.Date < DateTime.UtcNow)
                .Select(x => new PersonalDinnerListItem
                {
                    Host = "You",
                    DinnerId = x.Id,
                    Partner = DinnerHostPartner(x, userId),
                    Date = x.Date.ToShortDateString()
                }).ToList();

            return asGuest.Concat(asHost).ToList();
        }

        public ICollection<PersonalDinnerListItem> GetHostedDinnerList(Guid userId)
        {
            return _session.Query<Dinner>()
                .Where(x => x.User.Id == userId)
                .Where(x => x.Date > DateTime.UtcNow)
                .Select(x => new PersonalDinnerListItem
                {
                    ApplicantCount = x.Applicants.Count,
                    DinnerId = x.Id,
                    Pending = x.VerificationCode != null,
                    ApprovalToken = x.User.Id == userId ? null : x.VerificationCode,
                    Partner = DinnerHostPartner(x, userId),
                    Date = x.Date.ToShortDateString()
                }).ToList();
        }

        private string AppliedPartner(DinnerApplicant dinnerApplicant, Guid userId)
        {
            if (dinnerApplicant.Partner == null)
                return null;
            return dinnerApplicant.Partner.Id == userId
                ? dinnerApplicant.User.FullName()
                : dinnerApplicant.Partner.FullName();
        }

        private string DinnerHostPartner(Dinner dinner, Guid userId)
        {
            if (dinner.Partner == null)
                return null;
            return dinner.Partner.Id == userId
                ? dinner.User.FullName()
                : dinner.Partner.FullName();
        }

        private bool HasApplied(IEnumerable<DinnerApplicant> applicants, Guid userId)
        {
            return
                applicants.Any(
                    applicant =>
                    applicant.User.Id == userId || (applicant.Partner != null && applicant.Partner.Id == userId));
        }
    }
}
