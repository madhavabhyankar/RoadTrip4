namespace RoadTrip_4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        RoadTripId = c.Guid(nullable: false),
                        BorrowerId = c.Int(),
                        ExpenseDate = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Longitude = c.Double(),
                        Latitude = c.Double(),
                        City = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.BorrowerId)
                .ForeignKey("dbo.UserDetails", t => t.OwnerId)
                .ForeignKey("dbo.RoadTrips", t => t.RoadTripId)
                .Index(t => t.BorrowerId)
                .Index(t => t.OwnerId)
                .Index(t => t.RoadTripId);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        NickName = c.String(),
                        Email = c.String(),
                        UserProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserProfileId)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.RoadTrips",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Details = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        OwnerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.PersonToRoadTripMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoadTripId = c.Guid(nullable: false),
                        UserDetailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoadTrips", t => t.RoadTripId)
                .ForeignKey("dbo.UserDetails", t => t.UserDetailId)
                .Index(t => t.RoadTripId)
                .Index(t => t.UserDetailId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Invitations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvitingUserDetailId = c.Int(nullable: false),
                        GuestEmail = c.String(),
                        InvitationStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "RoadTripId", "dbo.RoadTrips");
            DropForeignKey("dbo.Expenses", "OwnerId", "dbo.UserDetails");
            DropForeignKey("dbo.Expenses", "BorrowerId", "dbo.UserDetails");
            DropForeignKey("dbo.UserDetails", "UserProfileId", "dbo.UserProfile");
            DropForeignKey("dbo.PersonToRoadTripMaps", "UserDetailId", "dbo.UserDetails");
            DropForeignKey("dbo.PersonToRoadTripMaps", "RoadTripId", "dbo.RoadTrips");
            DropForeignKey("dbo.RoadTrips", "OwnerId", "dbo.UserDetails");
            DropIndex("dbo.Expenses", new[] { "RoadTripId" });
            DropIndex("dbo.Expenses", new[] { "OwnerId" });
            DropIndex("dbo.Expenses", new[] { "BorrowerId" });
            DropIndex("dbo.UserDetails", new[] { "UserProfileId" });
            DropIndex("dbo.PersonToRoadTripMaps", new[] { "UserDetailId" });
            DropIndex("dbo.PersonToRoadTripMaps", new[] { "RoadTripId" });
            DropIndex("dbo.RoadTrips", new[] { "OwnerId" });
            DropTable("dbo.Invitations");
            DropTable("dbo.UserProfile");
            DropTable("dbo.PersonToRoadTripMaps");
            DropTable("dbo.RoadTrips");
            DropTable("dbo.UserDetails");
            DropTable("dbo.Expenses");
        }
    }
}
