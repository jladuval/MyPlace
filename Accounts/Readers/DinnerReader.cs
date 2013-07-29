namespace Accounts.Readers
{
    using Interfaces.Presentation.Dinner;
    using Interfaces.Readers;
    using NetTopologySuite.Geometries;
    using NHibernate;
    using NHibernate.Spatial.Type;

    public class DinnerReader : IDinnerReader
    {
        private readonly ISession _session;

        public DinnerReader(ISession session)
        {
            _session = session;
        }

        public DinnerListDto GetDinnerList(double lat, double lng)
        {
            var point = new Point(lat, lng);
            var result = _session.CreateQuery(@"select p from Dinner p 
                                   order by NHSP.Distance(p.Location.GeoLoc, :point)")
                    .SetParameter("point", point,
                        NHibernateUtil.Custom(typeof(MsSql2008GeometryType)))
                    .SetMaxResults(10)
                    .List();
            return new DinnerListDto();
        }
    }
}
