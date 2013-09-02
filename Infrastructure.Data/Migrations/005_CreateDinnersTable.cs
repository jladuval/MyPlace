namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    using Infrastructure.Data.Extensions;

    [Migration(201306172005)]
    public class CreateDinnersTable : Migration
    {
        public override void Up()
        {
            Create.Table("Dinners").InSchema("dbo")
                .WithColumn("Id")
                    .AsGuid()
                    .PrimaryKey()
                .WithColumn("CreatedDate")
                    .AsCustom("datetime2(7)")
                    .NotNullable()
                .WithColumn("ModifiedDate")
                    .AsCustom("datetime2(7)")
                    .Nullable()
                .WithColumn("Starter")
                    .AsMaxString()
                    .NotNullable()
                .WithColumn("Main")
                    .AsMaxString()
                    .NotNullable()
                .WithColumn("Dessert")
                    .AsMaxString()
                    .NotNullable()
                .WithColumn("Dry")
                    .AsBoolean()
                    .NotNullable()
                .WithColumn("Description")
                    .AsMaxString()
                    .NotNullable()
                .WithColumn("ClosedDate")
                    .AsCustom("datetime2(7)")
                    .Nullable()
                .WithColumn("Date")
                    .AsCustom("datetime2(7)")
                    .Nullable()
                .WithColumn("UserId")
                    .AsGuid()
                    .NotNullable()
                .WithColumn("LocationId")
                    .AsGuid()
                    .NotNullable();

            Create.ForeignKey("FK_dbo.Dinners_dbo.Location_Id")
                  .FromTable("Dinners")
                  .InSchema("dbo")
                  .ForeignColumn("LocationId")
                  .ToTable("Locations")
                  .InSchema("dbo")
                  .PrimaryColumn("Id");

            Create.ForeignKey("FK_dbo.Dinners_dbo.User_Id")
                  .FromTable("Dinners")
                  .InSchema("dbo")
                  .ForeignColumn("UserId")
                  .ToTable("users")
                  .InSchema("dbo").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Dinners").InSchema("dbo");
            Delete.ForeignKey("FK_dbo.Dinners_dbo.Location_Id")
                .OnTable("Dinners").InSchema("dbo");

            Delete.ForeignKey("FK_dbo.Dinners_dbo.User_Id")
                .OnTable("Dinners").InSchema("dbo");
        }
    }
}
