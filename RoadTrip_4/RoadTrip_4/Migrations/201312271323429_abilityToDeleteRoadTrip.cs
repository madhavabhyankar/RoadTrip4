namespace RoadTrip_4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abilityToDeleteRoadTrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoadTrips", "RoadTripStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoadTrips", "RoadTripStatus");
        }
    }
}
