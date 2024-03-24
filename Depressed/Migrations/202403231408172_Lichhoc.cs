namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lichhoc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lichhocs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Lichhocs", new[] { "UserId" });
            DropColumn("dbo.Lichhocs", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lichhocs", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Lichhocs", "UserId");
            AddForeignKey("dbo.Lichhocs", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
