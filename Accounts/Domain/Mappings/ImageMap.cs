using System;
using FluentNHibernate.Mapping;

namespace Accounts.Domain.Mappings
{
    public class ImageMap : ClassMap<Image>
    {
        public ImageMap()
        {
            Table("Images");

            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);

            Map(x => x.CreatedDate).Not.Nullable();

            Map(x => x.ModifiedDate).Not.Nullable();

            Map(x => x.FolderPath);

            Map(x => x.ImageName);

            Map(x => x.Url).Not.Nullable();
        }
    }
}
