namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ihope : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassMembers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        class_id = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Lophocs", t => t.class_id, cascadeDelete: true)
                .Index(t => t.class_id)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassMembers", "class_id", "dbo.Lophocs");
            DropForeignKey("dbo.ClassMembers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ClassMembers", new[] { "UserId" });
            DropIndex("dbo.ClassMembers", new[] { "class_id" });
            DropTable("dbo.ClassMembers");
        }
    }
}
