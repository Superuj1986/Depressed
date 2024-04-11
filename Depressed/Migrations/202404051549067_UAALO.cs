namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UAALO : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Main_Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        post_id = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        DateComment = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Post_Post", t => t.post_id, cascadeDelete: false)
                .Index(t => t.post_id)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Sub_Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        main_id = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        DateComment = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Main_Comment", t => t.main_id, cascadeDelete: false)
                .Index(t => t.main_id)
                .Index(t => t.UserId);
            
            AlterColumn("dbo.Lichhocs", "Ngayhoc1", c => c.String());
            AlterColumn("dbo.Lichhocs", "Ngayhoc2", c => c.String());
            AlterColumn("dbo.Lichhocs", "Ngayhoc3", c => c.String());
            DropColumn("dbo.RollOuts", "total_absent");
            DropColumn("dbo.RollOuts", "total_percent");
            DropTable("dbo.Thongbaos");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.RollOuts", "total_percent", c => c.Single(nullable: false));
            AddColumn("dbo.RollOuts", "total_absent", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sub_Comment", "main_id", "dbo.Main_Comment");
            DropForeignKey("dbo.Sub_Comment", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Main_Comment", "post_id", "dbo.Post_Post");
            DropForeignKey("dbo.Main_Comment", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Sub_Comment", new[] { "UserId" });
            DropIndex("dbo.Sub_Comment", new[] { "main_id" });
            DropIndex("dbo.Main_Comment", new[] { "UserId" });
            DropIndex("dbo.Main_Comment", new[] { "post_id" });
            AlterColumn("dbo.Lichhocs", "Ngayhoc3", c => c.DateTime());
            AlterColumn("dbo.Lichhocs", "Ngayhoc2", c => c.DateTime());
            AlterColumn("dbo.Lichhocs", "Ngayhoc1", c => c.DateTime());
            DropTable("dbo.Sub_Comment");
            DropTable("dbo.Main_Comment");
        }
    }
}
