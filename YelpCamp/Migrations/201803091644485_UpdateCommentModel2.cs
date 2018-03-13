namespace YelpCamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCommentModel2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "UserId", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Comments", name: "IX_UserId", newName: "IX_ApplicationUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_ApplicationUserId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Comments", name: "ApplicationUserId", newName: "UserId");
        }
    }
}
