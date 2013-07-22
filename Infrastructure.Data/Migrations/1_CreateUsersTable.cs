namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    [Migration(201305011300)]
    public class CreateUsersTable : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                  .InSchema("dbo")
                  .WithColumn("Id")
                      .AsGuid()
                      .PrimaryKey()
                  .WithColumn("CreatedDate")
                      .AsCustom("datetime2(7)")
                      .NotNullable()
                  .WithColumn("ModifiedDate")
                      .AsCustom("datetime2(7)")
                      .NotNullable()
                  .WithColumn("FirstName")
                      .AsString(255)
                      .Nullable()
                  .WithColumn("LastName")
                      .AsString(255)
                      .Nullable()
                  .WithColumn("Email")
                      .AsString(255)
                      .NotNullable()
                      .Unique("IX_Users_Email")
                  .WithColumn("Country")
                      .AsString(255)
                      .Nullable()
                  .WithColumn("Timezone")
                      .AsString(255)
                      .Nullable()
                  .WithColumn("Culture")
                      .AsString(255)
                      .Nullable()
                  .WithColumn("Password")
                      .AsString(255)
                      .Nullable()
                  .WithColumn("Salt")
                      .AsString(255)
                      .Nullable()
                  .WithColumn("VerificationCode")
                      .AsString(255)
                      .Nullable()
                  .WithColumn("IsVerified")
                      .AsBoolean()
                      .Nullable();
        }

        public override void Down()
        {
            Delete.Table("Users")
                .InSchema("dbo");
        }
    }
}


