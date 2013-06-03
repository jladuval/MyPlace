namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    [Migration(201305021645)]
    public class CreateUsersInRolesTable : Migration
    {
        public override void Up()
        {
            Create.Table("UsersInRoles")
                .InSchema("dbo")
                .WithColumn("UserId")
                    .AsGuid()
                    .NotNullable()
                    .Indexed("IX_UsersInRoles_UserId")
                .WithColumn("RoleId")
                    .AsGuid()
                    .NotNullable()
                    .Indexed("IX_UsersInRoles_RoleId");
            
            Create.PrimaryKey("PK_dbo.UsersInRoles")
                  .OnTable("UsersInRoles")
                  .WithSchema("dbo")
                  .Columns(new[] { "UserId", "RoleId" });
            
            Create.ForeignKey("FK_dbo.UsersInRoles_dbo.Users_Id")
                  .FromTable("UsersInRoles")
                  .InSchema("dbo")
                  .ForeignColumn("UserId")
                  .ToTable("Users")
                  .InSchema("dbo")
                  .PrimaryColumn("Id");
            
            Create.ForeignKey("FK_dbo.UsersInRoles_dbo.Roles_Id")
                  .FromTable("UsersInRoles")
                  .InSchema("dbo")
                  .ForeignColumn("RoleId")
                  .ToTable("Roles")
                  .InSchema("dbo")
                  .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_dbo.UsersInRoles_dbo.Users_Id")
                .OnTable("UsersInRoles")
                .InSchema("dbo");

            Delete.ForeignKey("FK_dbo.UsersInRoles_dbo.Roles_Id")
                .OnTable("UsersInRoles")
                .InSchema("dbo");

            Delete.Table("UsersInRoles")
                .InSchema("dbo");
            
        }
    }
}


