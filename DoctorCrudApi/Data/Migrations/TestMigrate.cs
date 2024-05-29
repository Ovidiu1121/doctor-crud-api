using FluentMigrator;

namespace DoctorCrudApi.Data.Migrations
{
    [Migration(29052024348)]
    public class TestMigrate: Migration
    {
        public override void Up()
        {
            Execute.Script(@"./Data/Scripts/data.sql");
        }
        public override void Down()
        {
            throw new NotImplementedException();
        }

    }
}
