namespace UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fullname = c.String(),
                        Birthday = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Journeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverId = c.Int(nullable: false),
                        RouteId = c.Int(nullable: false),
                        TransportId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .ForeignKey("dbo.Transports", t => t.TransportId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.RouteId)
                .Index(t => t.TransportId);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        journeyId = c.Int(nullable: false),
                        price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Journeys", t => t.journeyId, cascadeDelete: true)
                .Index(t => t.journeyId);
            
            CreateTable(
                "dbo.Transports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Journeys", "TransportId", "dbo.Transports");
            DropForeignKey("dbo.Tickets", "journeyId", "dbo.Journeys");
            DropForeignKey("dbo.Journeys", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Journeys", "DriverId", "dbo.Drivers");
            DropIndex("dbo.Tickets", new[] { "journeyId" });
            DropIndex("dbo.Journeys", new[] { "TransportId" });
            DropIndex("dbo.Journeys", new[] { "RouteId" });
            DropIndex("dbo.Journeys", new[] { "DriverId" });
            DropTable("dbo.Transports");
            DropTable("dbo.Tickets");
            DropTable("dbo.Routes");
            DropTable("dbo.Journeys");
            DropTable("dbo.Drivers");
        }
    }
}
