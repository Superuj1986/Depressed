namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moetoiquen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Main_Comment", "comments", c => c.String());
            AddColumn("dbo.Sub_Comment", "comments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sub_Comment", "comments");
            DropColumn("dbo.Main_Comment", "comments");
        }
    }
}
