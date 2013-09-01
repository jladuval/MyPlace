namespace Infrastructure.Data.Extensions
{
    using FluentMigrator.Builders.Create.Table;
    using FluentMigrator.Builders.Alter.Table;

    internal static class MigratorExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax AsMaxString(this ICreateTableColumnAsTypeSyntax createTableColumnAsTypeSyntax)
        {
            return createTableColumnAsTypeSyntax.AsString(int.MaxValue);
        }
    }
}
