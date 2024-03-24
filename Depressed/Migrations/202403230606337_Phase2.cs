namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Phase2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassMembers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        lophoc_id = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Lophocs", t => t.lophoc_id, cascadeDelete: false)
                .Index(t => t.lophoc_id)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Lophocs",
                c => new
                    {
                        class_id = c.Int(nullable: false, identity: true),
                        class_name = c.String(),
                        content = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.class_id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Lichhocs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        lophoc_id = c.Int(nullable: false),
                        Day1_Start = c.DateTime(),
                        Day1_End = c.DateTime(),
                        Day2_Start = c.DateTime(),
                        Day2_End = c.DateTime(),
                        Day3_Start = c.DateTime(),
                        Day3_End = c.DateTime(),
                        Day4_Start = c.DateTime(),
                        Day4_End = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Lophocs", t => t.lophoc_id, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.lophoc_id);
            
            CreateTable(
                "dbo.RollOuts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        lh_id = c.Int(nullable: false),
                        Lec_Content = c.String(nullable: false),
                        date = c.DateTime(),
                        excuted = c.Boolean(),
                        total_absent = c.Int(nullable: false),
                        total_percent = c.Single(nullable: false),
                        UserId = c.String(maxLength: 128),
                        lophoc_id = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Lichhocs", t => t.lh_id, cascadeDelete: false)
                .ForeignKey("dbo.Lophocs", t => t.lophoc_id, cascadeDelete: false)
                .Index(t => t.lh_id)
                .Index(t => t.UserId)
                .Index(t => t.lophoc_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RollOuts", "lophoc_id", "dbo.Lophocs");
            DropForeignKey("dbo.RollOuts", "lh_id", "dbo.Lichhocs");
            DropForeignKey("dbo.RollOuts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClassMembers", "lophoc_id", "dbo.Lophocs");
            DropForeignKey("dbo.Lichhocs", "lophoc_id", "dbo.Lophocs");
            DropForeignKey("dbo.Lichhocs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lophocs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClassMembers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.RollOuts", new[] { "lophoc_id" });
            DropIndex("dbo.RollOuts", new[] { "UserId" });
            DropIndex("dbo.RollOuts", new[] { "lh_id" });
            DropIndex("dbo.Lichhocs", new[] { "lophoc_id" });
            DropIndex("dbo.Lichhocs", new[] { "UserId" });
            DropIndex("dbo.Lophocs", new[] { "UserId" });
            DropIndex("dbo.ClassMembers", new[] { "UserId" });
            DropIndex("dbo.ClassMembers", new[] { "lophoc_id" });
            DropTable("dbo.RollOuts");
            DropTable("dbo.Lichhocs");
            DropTable("dbo.Lophocs");
            DropTable("dbo.ClassMembers");
        }
    }
}
