namespace YelpCamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCommentAndCampWithCreatAtDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Campgrounds", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "CreatedAt");
            DropColumn("dbo.Campgrounds", "CreatedAt");
        }
    }
}
