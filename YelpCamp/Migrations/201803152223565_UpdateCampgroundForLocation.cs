namespace YelpCamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCampgroundForLocation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Campgrounds", "LocationId", "dbo.Locations");
            DropIndex("dbo.Campgrounds", new[] { "LocationId" });
            AddColumn("dbo.Campgrounds", "Address", c => c.String());
            AddColumn("dbo.Campgrounds", "Lat", c => c.Double(nullable: false));
            AddColumn("dbo.Campgrounds", "Long", c => c.Double(nullable: false));
            DropColumn("dbo.Campgrounds", "LocationId");
            DropTable("dbo.Locations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Lat = c.Double(nullable: false),
                        Long = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Campgrounds", "LocationId", c => c.Int(nullable: false));
            DropColumn("dbo.Campgrounds", "Long");
            DropColumn("dbo.Campgrounds", "Lat");
            DropColumn("dbo.Campgrounds", "Address");
            CreateIndex("dbo.Campgrounds", "LocationId");
            AddForeignKey("dbo.Campgrounds", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
    }
}
