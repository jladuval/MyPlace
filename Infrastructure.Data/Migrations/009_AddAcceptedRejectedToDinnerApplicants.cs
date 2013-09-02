namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    [Migration(201308270836)]
    public class AddAcceptedRejectedToDinnerApplicants : Migration
    {
        public override void Up()
        {
            Alter.Table("DinnerApplicants")
                .AddColumn("Accepted")
                    .AsBoolean()
                    .NotNullable()
                    .WithDefaultValue(false)
                .AddColumn("Rejected")
                    .AsBoolean()
                    .NotNullable()
                    .WithDefaultValue(false);

        }

        public override void Down()
        {
            Delete.Column("Accepted").FromTable("DinnerApplicants");
            Delete.Column("Rejected").FromTable("DinnerApplicants");
        }
    }
}
