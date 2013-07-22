using FluentMigrator;

namespace Infrastructure.Data.Migrations
{
    [Migration(201307220850)]
    public class AddImages : Migration
    {
        public override void Up()
        {
            Create.Table("Images").InSchema("dbo")
                .WithColumn("Id")
                    .AsGuid()
                    .PrimaryKey()
                .WithColumn("CreatedDate")
                    .AsCustom("datetime2(7)")
                    .NotNullable()
                .WithColumn("ModifiedDate")
                    .AsCustom("datetime2(7)")
                    .NotNullable()
                .WithColumn("Url")
                    .AsString()
                    .NotNullable()
                .WithColumn("FolderPath")
                    .AsString()
                    .Nullable()
                .WithColumn("ImageName")
                    .AsString()
                    .Nullable()
                .WithColumn("Selected")
                    .AsBoolean()
                    .Nullable()
                .WithColumn("UserProfileId")
                    .AsGuid()
                    .Nullable()
                .WithColumn("UserHouseId")
                    .AsGuid()
                    .Nullable()
                .WithColumn("DinnerId")
                    .AsGuid()
                    .Nullable();

            Alter.Table("Users").InSchema("dbo")
                .AddColumn("ProfileImageUrl")
                .AsString()
                .Nullable();

            Alter.Table("Dinners").InSchema("dbo")
                .AddColumn("ImageUrl")
                .AsString()
                .Nullable();

            Create.ForeignKey("FK_dbo.Images_Profile_dbo.User_Id")
                  .FromTable("Images")
                  .InSchema("dbo")
                  .ForeignColumn("UserProfileId")
                  .ToTable("Users")
                  .InSchema("dbo")
                  .PrimaryColumn("Id");

            Create.ForeignKey("FK_dbo.Images_House_dbo.User_Id")
                  .FromTable("Images")
                  .InSchema("dbo")
                  .ForeignColumn("UserHouseId")
                  .ToTable("Users")
                  .InSchema("dbo")
                  .PrimaryColumn("Id");

            Create.ForeignKey("FK_dbo.Images_Dinner_dbo.User_Id")
                  .FromTable("Images")
                  .InSchema("dbo")
                  .ForeignColumn("DinnerId")
                  .ToTable("Dinners")
                  .InSchema("dbo")
                  .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Images").InSchema("dbo");
            Delete.ForeignKey("FK_dbo.Images_Profile_dbo.User_Id")
                .OnTable("Images").InSchema("dbo");

            Delete.ForeignKey("FK_dbo.Images_House_dbo.User_Id")
                .OnTable("Images").InSchema("dbo");

            Delete.ForeignKey("FK_dbo.Images_Dinner_dbo.User_Id")
                .OnTable("Images").InSchema("dbo");

            Delete.Column("ProfileImageUrl")
                .FromTable("Users").InSchema("dbo");

            Delete.Column("DinnerImageUrl")
                .FromTable("Dinners").InSchema("dbo");
        }
    }
}
