namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    [Migration(201309030755)]
    public class AddAgeToUser : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("Age")
                    .AsString(int.MaxValue)
                    .Nullable();
        }

        public override void Down()
        {
            Delete.Column("Age").FromTable("Users");
        }
    }
}
