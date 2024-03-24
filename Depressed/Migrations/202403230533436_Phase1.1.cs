namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Phase11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ca_id = c.Int(nullable: false, identity: true),
                        ca_name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ca_id);
            
            CreateTable(
                "dbo.Khoahocs",
                c => new
                    {
                        kh_id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        name = c.String(),
                        category_id = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        Image = c.Binary(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.kh_id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Categories", t => t.category_id, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.category_id);
            
            CreateTable(
                "dbo.Thongbaos",
                c => new
                    {
                        tb_id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        content = c.String(nullable: false),
                        Date_Public = c.DateTime(),
                    })
                .PrimaryKey(t => t.tb_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Khoahocs", "category_id", "dbo.Categories");
            DropForeignKey("dbo.Khoahocs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Khoahocs", new[] { "category_id" });
            DropIndex("dbo.Khoahocs", new[] { "UserId" });
            DropTable("dbo.Thongbaos");
            DropTable("dbo.Khoahocs");
            DropTable("dbo.Categories");
        }
    }
}
