namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    [Migration(201309041326)]
    public class AddPartnerToApplication : Migration
    {
        public override void Up()
        {
            Alter.Table("DinnerApplicants")
                .AddColumn("VerificationCode")
                    .AsGuid()
                    .Nullable()
                .AddColumn("PartnerId")
                    .AsGuid()
                    .Nullable();

            Create.ForeignKey("FK_dbo.DinnerApplicants_dbo.Partner_Id")
                  .FromTable("DinnerApplicants")
                  .InSchema("dbo")
                  .ForeignColumn("PartnerId")
                  .ToTable("users")
                  .InSchema("dbo").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_dbo.DinnerApplicants_dbo.Partner_Id").OnTable("DinnerApplicants");
            Delete.Column("VerificationCode").Column("PartnerId").FromTable("DinnerApplicants");
        }
    }
}
