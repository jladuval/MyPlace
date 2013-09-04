namespace Accounts.Domain.Mappings
{
    using System;

    using FluentNHibernate.Mapping;

    public class DinnerApplicantMap : ClassMap<DinnerApplicant>
    {
        public DinnerApplicantMap()
        {
            Table("DinnerApplicants");

            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);

            References(x => x.User).Column("UserId").Not.Nullable().LazyLoad(Laziness.False);

            References(x => x.Dinner).Column("DinnerId").Not.Nullable().LazyLoad(Laziness.False);

            References(x => x.Partner).Column("PartnerId").Nullable().Cascade.SaveUpdate();

            Map(x => x.VerificationCode).Nullable();

            Map(x => x.Accepted).Not.Nullable();

            Map(x => x.Rejected).Not.Nullable();

        }
    }
}
