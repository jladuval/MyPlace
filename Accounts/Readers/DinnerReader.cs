namespace Accounts.Readers
{
    using System;
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

        public DinnerDto GetDinner(Guid id)
        {
            Mapper.CreateMap<Dinner, DinnerDto>();
            return Mapper.Map<DinnerDto>(_session.Query<Dinner>().Single(x => x.Id == id));
        }
    }
}
