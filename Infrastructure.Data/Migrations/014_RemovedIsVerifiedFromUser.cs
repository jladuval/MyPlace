namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    [Migration(201309081655)]
    public class RemovedIsVerifiedFromUser : Migration
    {
        public override void Up()
        {
            Delete.Column("IsVerified").FromTable("Users");
        }

        public override void Down()
        {
            Alter.Table("Users").AddColumn("IsVerified").AsBoolean().Nullable();
        }
    }
}
