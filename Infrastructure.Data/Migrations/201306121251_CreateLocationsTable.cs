namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    [Migration(201306121251)]
    public class CreateLocationsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Locations").InSchema("dbo")
                .WithColumn("Id")
                    .AsGuid()
                    .PrimaryKey()
                .WithColumn("Address")
                    .AsString(255)
                    .NotNullable()
                .WithColumn("Suburb")
                    .AsString(255)
                    .NotNullable()
                .WithColumn("City")
                    .AsString(255)
                    .NotNullable()
                .WithColumn("Country")
                    .AsString(255)
                    .NotNullable()
                .WithColumn("Postcode")
                    .AsString(255)
                    .NotNullable();

            Alter.Table("Users").InSchema("dbo")
                .AddColumn("LocationId")
                    .AsGuid()
                    .Nullable();

            Create.ForeignKey("FK_dbo.Users_dbo.Locations_Id")
                  .FromTable("Users")
                  .InSchema("dbo")
                  .ForeignColumn("LocationId")
                  .ToTable("Locations")
                  .InSchema("dbo")
                  .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Locations").InSchema("dbo");
            Delete.ForeignKey("FK_dbo.Users_dbo.Locations_Id")
                .OnTable("Users").InSchema("dbo");
        }
    }
}
