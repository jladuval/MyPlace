namespace Accounts.Domain.Mappings
{
    using System;
    using FluentNHibernate.Mapping;

    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {
            Table("Comments");

            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);

            Map(x => x.CreatedDate).Not.Nullable();

            Map(x => x.ModifiedDate).Not.Nullable();

            References(x => x.User).Column("UserId").Not.Nullable().Cascade.All();

            Map(x => x.Text).Not.Nullable();
        }
    }
}
