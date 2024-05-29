using FluentMigrator;

namespace DoctorCrudApi.Data.Migrations
{
    [Migration(29052024)]
    public class CreateSchema : Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Create.Table("doctor")
                 .WithColumn("id").AsInt32().PrimaryKey().Identity()
                  .WithColumn("name").AsString(128).NotNullable()
                   .WithColumn("type").AsString(128).NotNullable()
                    .WithColumn("patients").AsInt32().NotNullable();
        }
    }
}
