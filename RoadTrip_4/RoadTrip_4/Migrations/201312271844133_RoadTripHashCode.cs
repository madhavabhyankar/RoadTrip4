namespace RoadTrip_4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoadTripHashCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoadTrips", "RoadTripHashId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoadTrips", "RoadTripHashId");
        }
    }
}
