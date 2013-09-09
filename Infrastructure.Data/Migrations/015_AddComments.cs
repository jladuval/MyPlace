namespace Infrastructure.Data.Migrations
{
    using Extensions;
    using FluentMigrator;

    [Migration(201309091836)]
    public class AddComments : Migration
    {
        public override void Up()
        {
            Create.Table("Comments")
                .WithColumn("Id")
                      .AsGuid()
                      .PrimaryKey()
                .WithColumn("CreatedDate")
                    .AsCustom("datetime2(7)")
                    .NotNullable()
                .WithColumn("ModifiedDate")
                    .AsCustom("datetime2(7)")
                    .NotNullable()
                .WithColumn("UserId")
                    .AsGuid()
                    .NotNullable()
                .WithColumn("DinnerId")
                    .AsGuid()
                    .Nullable()
                .WithColumn("Text")
                    .AsMaxString()
                    .NotNullable();

            Create.ForeignKey("FK_dbo.Comments_dbo.User_Id")
                  .FromTable("Comments")
                  .InSchema("dbo")
                  .ForeignColumn("UserId")
                  .ToTable("users")
                  .InSchema("dbo").PrimaryColumn("Id");

            Create.ForeignKey("FK_dbo.Comments_dbo.Dinner_Id")
                  .FromTable("Comments")
                  .InSchema("dbo")
                  .ForeignColumn("DinnerId")
                  .ToTable("Dinners")
                  .InSchema("dbo").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_dbo.Comments_dbo.Dinner_Id");
            Delete.ForeignKey("FK_dbo.Comments_dbo.User_Id");
            Delete.Table("Comments");
        }
    }
}
