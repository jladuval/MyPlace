namespace Accounts.Readers
{
    using Base.CQRS.Query.Attributes;
    using Interfaces.Presentation.Dinner;
    using Interfaces.Readers;
    using NHibernate;
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
                    FETCH NEXT {3} ROWS ONLY"
                , lat, lng, skip, take);
            var output = _session
                .CreateSQLQuery(selectDinnerByDistance)
                .SetResultTransformer(Transformers.AliasToBean<DinnerListItemDto>())
                .List<DinnerListItemDto>();
            return new DinnerListDto();
        }
    }
}
