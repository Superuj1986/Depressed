namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Checking : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pre_Post", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Pre_Post", new[] { "UserId" });
            AddColumn("dbo.Post_Post", "isApproved", c => c.Boolean());
            DropTable("dbo.Pre_Post");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Pre_Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        UserId = c.String(maxLength: 128),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Post_Post", "isApproved");
            CreateIndex("dbo.Pre_Post", "UserId");
            AddForeignKey("dbo.Pre_Post", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
