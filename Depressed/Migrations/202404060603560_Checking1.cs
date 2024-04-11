namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Checking1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post_Post", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Post_Post", "isApproved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Post_Post", "isApproved", c => c.Boolean());
            DropColumn("dbo.Post_Post", "Status");
        }
    }
}
