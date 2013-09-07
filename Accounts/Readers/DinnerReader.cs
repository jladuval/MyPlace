﻿namespace Accounts.Readers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Accounts.Domain;
    using AutoMapper;
    using Base.CQRS.Query.Attributes;
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

        private bool HasApplied(IEnumerable<DinnerApplicant> applicants, Guid userId)
        {
            return
                applicants.Any(
                    applicant =>
                    applicant.User.Id == userId /*|| (applicant.Partner != null && applicant.Partner.Id == userId)*/);
        }
    }
}
