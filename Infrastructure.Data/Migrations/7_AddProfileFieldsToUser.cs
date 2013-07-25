namespace Infrastructure.Data.Migrations
{
    using Common.Enums;
    using FluentMigrator;

    [Migration(201307252051)]
    public class AddProfileFieldsToUser : Migration
    {
        public override void Up()
        {
            Alter.Table("Users").InSchema("dbo")
                .AddColumn("Description")
                    .AsString()
                    .Nullable()
                .AddColumn("Orientation")
                    .AsString()
                    .NotNullable()
                    .WithDefaultValue(Orientation.Straight)
                .AddColumn("Gender")
                    .AsString()
                    .NotNullable()
                    .WithDefaultValue(Gender.Male)
                .AddColumn("Romance")
                    .AsBoolean()
                    .NotNullable()
                    .WithDefaultValue(false)
                .AddColumn("Friendship")
                    .AsBoolean()
                    .NotNullable()
                    .WithDefaultValue(false);
        }

        public override void Down()
        {
            Delete
                .Column("Description")
                .Column("Orientation")
                .Column("Gender")
                .Column("Romance")
                .Column("Friendship")
                .FromTable("Users").InSchema("dbo");
        }
    }
}
