namespace UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "journeyId", "dbo.Journeys");
            DropIndex("dbo.Tickets", new[] { "journeyId" });
            AddColumn("dbo.Journeys", "TicketsCount", c => c.Int(nullable: false));
            AddColumn("dbo.Journeys", "TicketPrice", c => c.Single(nullable: false));
            DropTable("dbo.Tickets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        journeyId = c.Int(nullable: false),
                        price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Journeys", "TicketPrice");
            DropColumn("dbo.Journeys", "TicketsCount");
            CreateIndex("dbo.Tickets", "journeyId");
            AddForeignKey("dbo.Tickets", "journeyId", "dbo.Journeys", "Id", cascadeDelete: true);
        }
    }
}
