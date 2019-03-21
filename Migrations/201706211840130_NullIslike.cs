namespace RoastMeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullIslike : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Votes", "IsLike", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Votes", "IsLike", c => c.Boolean(nullable: false));
        }
    }
}
