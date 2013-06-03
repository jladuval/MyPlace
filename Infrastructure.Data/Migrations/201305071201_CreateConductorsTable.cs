using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLQR.Infrastructure.Data.Migrations
{
    [Migration(201305071201)]
    public class CreateConductorsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Conductors")
                .InSchema("dbo")
                .WithColumn("Id")
                    .AsGuid()
                    .ForeignKey("Users", "Id")
                .WithColumn("CompanyName")
                    .AsString(255)
                .WithColumn("CompanyRole")
                    .AsString(255)
                .WithColumn("PhoneNumber")
                    .AsString(255);
        }

        public override void Down()
        {
            Delete.Table("Conductors")
                .InSchema("dbo");
        }
    }
}
