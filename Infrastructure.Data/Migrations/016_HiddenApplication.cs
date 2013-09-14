namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    [Migration(201309142120)]
    public class HiddenApplication : Migration
    {
        public override void Up()
        {
            Alter.Table("DinnerApplicants")
                .AddColumn("Hidden")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(false);
        }

        public override void Down()
        {
            Delete.Column("Hidden")
                .FromTable("DinnerApplicants");
        }
    }
}
