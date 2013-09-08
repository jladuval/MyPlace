namespace Base.Mailing.Entities.Mappings
{
    using System;

    using FluentNHibernate.Mapping;

    public class EmailMap : ClassMap<Email>
    {
        public EmailMap()
        {
            Table("Emails");

            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);

            Map(x => x.CreatedDate).Not.Nullable();

            Map(x => x.ModifiedDate).Not.Nullable();

            Map(x => x.Address).Not.Nullable();

            Map(x => x.Payload).Nullable();

            Map(x => x.SentDate).Nullable();

            Map(x => x.Priority).Not.Nullable();

            Map(x => x.TemplateName).Not.Nullable();
        }
    }
}
