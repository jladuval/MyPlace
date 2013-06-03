namespace Infrastructure.NHibernate.Conventions
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;

    internal class SqlTimestampConvention : IVersionConvention
    {
        public void Apply(IVersionInstance instance)
        {
            if (instance.Type.Name == "BinaryBlob")
            {
                instance.Nullable();
                instance.CustomSqlType("timestamp");
                instance.Generated.Always();
            }
        }
    }
}