namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    using Infrastructure.Data.Extensions;

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
                      .AsMaxString()
                      .Nullable()
                  .WithColumn("LastName")
                      .AsMaxString()
                      .Nullable()
                  .WithColumn("Email")
                      .AsString(400)
                      .NotNullable()
                      .Unique("IX_Users_Email")
                  .WithColumn("Country")
                      .AsMaxString()
                      .Nullable()
                  .WithColumn("Timezone")
                      .AsMaxString()
                      .Nullable()
                  .WithColumn("Culture")
                      .AsMaxString()
                      .Nullable()
                  .WithColumn("Password")
                      .AsMaxString()
                      .Nullable()
                  .WithColumn("Salt")
                      .AsMaxString()
                      .Nullable()
                  .WithColumn("VerificationCode")
                      .AsMaxString()
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


