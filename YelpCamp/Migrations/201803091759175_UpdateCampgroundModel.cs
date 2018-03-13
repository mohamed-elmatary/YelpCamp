namespace YelpCamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCampgroundModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Campgrounds", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Campgrounds", "ApplicationUserId");
            AddForeignKey("dbo.Campgrounds", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Campgrounds", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Campgrounds", new[] { "ApplicationUserId" });
            DropColumn("dbo.Campgrounds", "ApplicationUserId");
        }
    }
}
