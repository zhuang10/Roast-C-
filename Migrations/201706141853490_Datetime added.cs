namespace RoastMeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Datetimeadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "time");
        }
    }
}
