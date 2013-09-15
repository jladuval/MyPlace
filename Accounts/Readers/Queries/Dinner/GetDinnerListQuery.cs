namespace Accounts.Readers.Queries.Dinner
{
    using System;
    using System.Linq;
    using Domain;
    using Interfaces.Presentation.Dinner;
    using NHibernate;
    using NHibernate.Linq;
    using NHibernate.Transform;

    internal class GetDinnerListQuery
    {
        private readonly ISession _session;

        public GetDinnerListQuery(ISession session)
        {
            _session = session;
        }

        public DinnerListDto Execute(double lat, double lng, int skip, int take)
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
                    d.VerificationCode as Token,
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
                .List<DinnerListItemDto>()
                .Where(x => x.Token == null)
                .ToList();

            var total = _session.Query<Dinner>().Count(x => x.Date < DateTime.UtcNow);

            return new DinnerListDto
                       {
                           TotalPages = total / take,
                           TotalResults = total,
                           Dinners = dinnerListByDistance
                       };
        }
    }
}
