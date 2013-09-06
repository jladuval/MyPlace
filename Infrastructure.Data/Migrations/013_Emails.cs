namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    using Infrastructure.Data.Extensions;

    [Migration(201309062010)]
    public class Emails : Migration
    {
        public override void Up()
        {
            Create.Table("Emails")
                .WithColumn("Id")
                      .AsGuid()
                      .PrimaryKey()
                .WithColumn("CreatedDate")
                    .AsCustom("datetime2(7)")
                    .NotNullable()
                .WithColumn("ModifiedDate")
                    .AsCustom("datetime2(7)")
                    .NotNullable()
                .WithColumn("SentDate")
                    .AsCustom("datetime2(7)")
                    .Nullable()
                .WithColumn("Payload")
                    .AsMaxString()
                    .Nullable()
                .WithColumn("Address")
                    .AsMaxString()
                    .NotNullable()
                .WithColumn("TemplateName")
                    .AsString()
                    .NotNullable()
                .WithColumn("Priority")
                    .AsInt32()
                    .NotNullable();
                
        }

        public override void Down()
        {
            Delete.Table("Emails");
        }
    }
}
