namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    [Migration(201307082044)]
    public class AddDinnerApplicationJunctionTable : Migration
    {
        public override void Up()
        {
            Create.Table("DinnerApplicants").InSchema("dbo")
                .WithColumn("UserId")
                    .AsGuid()
                    .NotNullable()
                .WithColumn("DinnerId")
                    .AsGuid()
                    .NotNullable();

            var compKey = new[] { "UserId", "DinnerId" };
            Create.PrimaryKey("PK_DinnerApplicants").OnTable("DinnerApplicants").Columns(compKey);

            Create.ForeignKey("FK_DinnerApplicants_User")
                .FromTable("DinnerApplicants")
                .ForeignColumn("UserId")
                .ToTable("Users")
                .PrimaryColumn("Id");
            Create.ForeignKey("FK_DinnerApplicants_Dinner")
                .FromTable("DinnerApplicants")
                .ForeignColumn("DinnerId")
                .ToTable("Dinners")
                .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_UserRole_User").OnTable("DinnerApplicants");
            Delete.ForeignKey("FK_UserRole_Role").OnTable("DinnerApplicants");
            Delete.Table("DinnerApplicants");
        }
    }
}
