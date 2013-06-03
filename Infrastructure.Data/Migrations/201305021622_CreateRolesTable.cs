namespace Infrastructure.Data.Migrations
{
    using System;

    using FluentMigrator;

    [Migration(201305021622)]
    public class CreateRolesTable : Migration
    {
        public override void Up()
        {
            Create.Table("Roles")
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
                .WithColumn("Name")
                    .AsString(255)
                    .NotNullable()
                    .Unique("IX_Roles_Name");

            Insert.IntoTable("Roles")
                .InSchema("dbo")
                .Row(new { Id = "15071a40-b2e8-11e2-9e96-0800200c9a66", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, Name = "Administrator" })
                .Row(new { Id = "15071a41-b2e8-11e2-9e96-0800200c9a66", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, Name = "User" });
        }

        public override void Down()
        {
            Delete.Table("Roles")
                .InSchema("dbo");
        }
    }
}


