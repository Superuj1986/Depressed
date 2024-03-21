namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendIdentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Khoahocs",
                c => new
                    {
                        kh_id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        te_id = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.kh_id)
                .ForeignKey("dbo.Teachers", t => t.te_id, cascadeDelete: false)
                .Index(t => t.te_id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        te_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        age = c.Int(nullable: false),
                        main_subject = c.String(nullable: false),
                        address = c.String(),
                        phone_number = c.Int(nullable: false),
                        BirthDate = c.DateTime(),
                        Image = c.Binary(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.te_id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Lophocs",
                c => new
                    {
                        class_id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        te_id = c.Int(nullable: false),
                        stu_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.class_id)
                .ForeignKey("dbo.Students", t => t.stu_id, cascadeDelete: false)
                .ForeignKey("dbo.Teachers", t => t.te_id, cascadeDelete: false)
                .Index(t => t.te_id)
                .Index(t => t.stu_id);
            
            CreateTable(
                "dbo.RollOuts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Lec_Content = c.String(nullable: false),
                        date = c.DateTime(),
                        excuted = c.Boolean(),
                        total_absent = c.Int(nullable: false),
                        total_percent = c.Single(nullable: false),
                        stu_id = c.Int(nullable: false),
                        te_id = c.Int(nullable: false),
                        class_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Lophocs", t => t.class_id, cascadeDelete: false)
                .ForeignKey("dbo.Students", t => t.stu_id, cascadeDelete: false)
                .ForeignKey("dbo.Teachers", t => t.te_id, cascadeDelete: false)
                .Index(t => t.stu_id)
                .Index(t => t.te_id)
                .Index(t => t.class_id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        stu_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        age = c.Int(nullable: false),
                        main_subject = c.String(nullable: false),
                        address = c.String(),
                        phone_number = c.Int(nullable: false),
                        BirthDate = c.DateTime(),
                        Image = c.Binary(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.stu_id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Thongbaos",
                c => new
                    {
                        tb_id = c.Int(nullable: false, identity: true),
                        te_id = c.Int(nullable: false),
                        title = c.String(nullable: false),
                        content = c.String(nullable: false),
                        Date_Public = c.DateTime(),
                    })
                .PrimaryKey(t => t.tb_id)
                .ForeignKey("dbo.Teachers", t => t.te_id, cascadeDelete: false)
                .Index(t => t.te_id);
            
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "BirthDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            DropForeignKey("dbo.Khoahocs", "te_id", "dbo.Teachers");
            DropForeignKey("dbo.Thongbaos", "te_id", "dbo.Teachers");
            DropForeignKey("dbo.Lophocs", "te_id", "dbo.Teachers");
            DropForeignKey("dbo.Lophocs", "stu_id", "dbo.Students");
            DropForeignKey("dbo.RollOuts", "te_id", "dbo.Teachers");
            DropForeignKey("dbo.RollOuts", "stu_id", "dbo.Students");
            DropForeignKey("dbo.Students", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RollOuts", "class_id", "dbo.Lophocs");
            DropForeignKey("dbo.Teachers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Thongbaos", new[] { "te_id" });
            DropIndex("dbo.Students", new[] { "UserId" });
            DropIndex("dbo.RollOuts", new[] { "class_id" });
            DropIndex("dbo.RollOuts", new[] { "te_id" });
            DropIndex("dbo.RollOuts", new[] { "stu_id" });
            DropIndex("dbo.Lophocs", new[] { "stu_id" });
            DropIndex("dbo.Lophocs", new[] { "te_id" });
            DropIndex("dbo.Teachers", new[] { "UserId" });
            DropIndex("dbo.Khoahocs", new[] { "te_id" });
            DropTable("dbo.Thongbaos");
            DropTable("dbo.Students");
            DropTable("dbo.RollOuts");
            DropTable("dbo.Lophocs");
            DropTable("dbo.Teachers");
            DropTable("dbo.Khoahocs");
        }
    }
}
