namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    [Migration(201309032126)]
    public class AddPartnerToDinner : Migration
    {
        public override void Up()
        {
            Alter.Table("Dinners")
                .AddColumn("VerificationCode")
                    .AsGuid()
                    .Nullable()
                .AddColumn("PartnerId")
                    .AsGuid()
                    .Nullable();

            Create.ForeignKey("FK_dbo.Dinners_dbo.Partner_Id")
                  .FromTable("Dinners")
                  .InSchema("dbo")
                  .ForeignColumn("PartnerId")
                  .ToTable("users")
                  .InSchema("dbo").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_dbo.Dinners_dbo.Partner_Id").OnTable("Dinners");
            Delete.Column("VerificationCode").Column("PartnerId").FromTable("Dinners");
        }
    }
}
