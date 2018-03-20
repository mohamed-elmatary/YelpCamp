namespace YelpCamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedDatabase : DbMigration
    {
        public override void Up()
        {
            Sql(@"insert into locations values('cairo',30.044420,31.235712)");
        }
        
        public override void Down()
        {
        }
    }
}
