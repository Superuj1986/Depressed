namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
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
            DropForeignKey("dbo.RollOuts", "te_id", "dbo.Teachers");
            DropForeignKey("dbo.Lophocs", "stu_id", "dbo.Students");
            DropForeignKey("dbo.Thongbaos", "te_id", "dbo.Teachers");
            DropIndex("dbo.Lophocs", new[] { "stu_id" });
            DropIndex("dbo.RollOuts", new[] { "te_id" });
            DropIndex("dbo.Thongbaos", new[] { "te_id" });
            RenameColumn(table: "dbo.Thongbaos", name: "te_id", newName: "Teacher_te_id");
            CreateTable(
                "dbo.Lichhocs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        te_id = c.Int(nullable: false),
                        class_id = c.Int(nullable: false),
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
                .ForeignKey("dbo.Lophocs", t => t.class_id, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.te_id, cascadeDelete: true)
                .Index(t => t.te_id)
                .Index(t => t.class_id);
            
            CreateTable(
                "dbo.ClassMembers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        class_id = c.Int(nullable: false),
                        stu_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Lophocs", t => t.class_id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.stu_id, cascadeDelete: true)
                .Index(t => t.class_id)
                .Index(t => t.stu_id);
            
            AddColumn("dbo.Khoahocs", "subject_name", c => c.String(nullable: false));
            AddColumn("dbo.Khoahocs", "Image", c => c.Binary());
            AddColumn("dbo.RollOuts", "lh_id", c => c.Int(nullable: false));
            AddColumn("dbo.RollOuts", "Note", c => c.String());
            AlterColumn("dbo.Students", "BirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Thongbaos", "Teacher_te_id", c => c.Int());
            CreateIndex("dbo.Thongbaos", "Teacher_te_id");
            CreateIndex("dbo.RollOuts", "lh_id");
            AddForeignKey("dbo.RollOuts", "lh_id", "dbo.Lichhocs", "id", cascadeDelete: true);
            AddForeignKey("dbo.Thongbaos", "Teacher_te_id", "dbo.Teachers", "te_id");
            DropColumn("dbo.Khoahocs", "End");
            DropColumn("dbo.Lophocs", "stu_id");
            DropColumn("dbo.RollOuts", "te_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RollOuts", "te_id", c => c.Int(nullable: false));
            AddColumn("dbo.Lophocs", "stu_id", c => c.Int(nullable: false));
            AddColumn("dbo.Khoahocs", "End", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Thongbaos", "Teacher_te_id", "dbo.Teachers");
            DropForeignKey("dbo.ClassMembers", "stu_id", "dbo.Students");
            DropForeignKey("dbo.ClassMembers", "class_id", "dbo.Lophocs");
            DropForeignKey("dbo.RollOuts", "lh_id", "dbo.Lichhocs");
            DropForeignKey("dbo.Lichhocs", "te_id", "dbo.Teachers");
            DropForeignKey("dbo.Lichhocs", "class_id", "dbo.Lophocs");
            DropIndex("dbo.ClassMembers", new[] { "stu_id" });
            DropIndex("dbo.ClassMembers", new[] { "class_id" });
            DropIndex("dbo.RollOuts", new[] { "lh_id" });
            DropIndex("dbo.Lichhocs", new[] { "class_id" });
            DropIndex("dbo.Lichhocs", new[] { "te_id" });
            DropIndex("dbo.Thongbaos", new[] { "Teacher_te_id" });
            AlterColumn("dbo.Thongbaos", "Teacher_te_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "BirthDate", c => c.DateTime());
            DropColumn("dbo.RollOuts", "Note");
            DropColumn("dbo.RollOuts", "lh_id");
            DropColumn("dbo.Khoahocs", "Image");
            DropColumn("dbo.Khoahocs", "subject_name");
            DropTable("dbo.ClassMembers");
            DropTable("dbo.Lichhocs");
            RenameColumn(table: "dbo.Thongbaos", name: "Teacher_te_id", newName: "te_id");
            CreateIndex("dbo.Thongbaos", "te_id");
            CreateIndex("dbo.RollOuts", "te_id");
            CreateIndex("dbo.Lophocs", "stu_id");
            AddForeignKey("dbo.Thongbaos", "te_id", "dbo.Teachers", "te_id", cascadeDelete: true);
            AddForeignKey("dbo.Lophocs", "stu_id", "dbo.Students", "stu_id", cascadeDelete: true);
            AddForeignKey("dbo.RollOuts", "te_id", "dbo.Teachers", "te_id", cascadeDelete: true);
        }
    }
}
