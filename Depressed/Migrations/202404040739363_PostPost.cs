namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostPost : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Posts", newName: "Post_Post");
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Post_Post", "IsApproved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Post_Post", "IsApproved", c => c.Boolean(nullable: false));
            DropIndex("dbo.Pre_Post", new[] { "UserId" });
            DropTable("dbo.Pre_Post");
            RenameTable(name: "dbo.Post_Post", newName: "Posts");
        }
    }
}
