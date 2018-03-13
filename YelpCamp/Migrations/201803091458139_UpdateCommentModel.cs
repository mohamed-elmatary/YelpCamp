namespace YelpCamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCommentModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "UserName", c => c.String());
        }
    }
}
