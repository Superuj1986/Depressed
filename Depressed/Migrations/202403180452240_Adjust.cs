namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adjust : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teachers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Thongbaos", "Teacher_te_id", "dbo.Teachers");
            DropForeignKey("dbo.Khoahocs", "te_id", "dbo.Teachers");
            DropForeignKey("dbo.Lichhocs", "te_id", "dbo.Teachers");
            DropForeignKey("dbo.Lophocs", "te_id", "dbo.Teachers");
            DropForeignKey("dbo.Students", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClassMembers", "class_id", "dbo.Lophocs");
            DropForeignKey("dbo.ClassMembers", "stu_id", "dbo.Students");
            DropForeignKey("dbo.RollOuts", "stu_id", "dbo.Students");
            DropIndex("dbo.Khoahocs", new[] { "te_id" });
            DropIndex("dbo.Teachers", new[] { "UserId" });
            DropIndex("dbo.Thongbaos", new[] { "Teacher_te_id" });
            DropIndex("dbo.Lophocs", new[] { "te_id" });
            DropIndex("dbo.Lichhocs", new[] { "te_id" });
            DropIndex("dbo.RollOuts", new[] { "stu_id" });
            DropIndex("dbo.Students", new[] { "UserId" });
            DropIndex("dbo.ClassMembers", new[] { "class_id" });
            DropIndex("dbo.ClassMembers", new[] { "stu_id" });
            AddColumn("dbo.Khoahocs", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Lophocs", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Lichhocs", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.RollOuts", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Khoahocs", "UserId");
            CreateIndex("dbo.Lophocs", "UserId");
            CreateIndex("dbo.Lichhocs", "UserId");
            CreateIndex("dbo.RollOuts", "UserId");
            AddForeignKey("dbo.Khoahocs", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Lophocs", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Lichhocs", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.RollOuts", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Khoahocs", "te_id");
            DropColumn("dbo.Thongbaos", "Teacher_te_id");
            DropColumn("dbo.Lophocs", "te_id");
            DropColumn("dbo.Lichhocs", "te_id");
            DropColumn("dbo.RollOuts", "stu_id");
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.ClassMembers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ClassMembers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        class_id = c.Int(nullable: false),
                        stu_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
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
                        BirthDate = c.DateTime(nullable: false),
                        Image = c.Binary(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.stu_id);
            
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
                .PrimaryKey(t => t.te_id);
            
            AddColumn("dbo.RollOuts", "stu_id", c => c.Int(nullable: false));
            AddColumn("dbo.Lichhocs", "te_id", c => c.Int(nullable: false));
            AddColumn("dbo.Lophocs", "te_id", c => c.Int(nullable: false));
            AddColumn("dbo.Thongbaos", "Teacher_te_id", c => c.Int());
            AddColumn("dbo.Khoahocs", "te_id", c => c.Int(nullable: false));
            DropForeignKey("dbo.RollOuts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lichhocs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lophocs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Khoahocs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.RollOuts", new[] { "UserId" });
            DropIndex("dbo.Lichhocs", new[] { "UserId" });
            DropIndex("dbo.Lophocs", new[] { "UserId" });
            DropIndex("dbo.Khoahocs", new[] { "UserId" });
            DropColumn("dbo.RollOuts", "UserId");
            DropColumn("dbo.Lichhocs", "UserId");
            DropColumn("dbo.Lophocs", "UserId");
            DropColumn("dbo.Khoahocs", "UserId");
            CreateIndex("dbo.ClassMembers", "stu_id");
            CreateIndex("dbo.ClassMembers", "class_id");
            CreateIndex("dbo.Students", "UserId");
            CreateIndex("dbo.RollOuts", "stu_id");
            CreateIndex("dbo.Lichhocs", "te_id");
            CreateIndex("dbo.Lophocs", "te_id");
            CreateIndex("dbo.Thongbaos", "Teacher_te_id");
            CreateIndex("dbo.Teachers", "UserId");
            CreateIndex("dbo.Khoahocs", "te_id");
            AddForeignKey("dbo.RollOuts", "stu_id", "dbo.Students", "stu_id", cascadeDelete: true);
            AddForeignKey("dbo.ClassMembers", "stu_id", "dbo.Students", "stu_id", cascadeDelete: true);
            AddForeignKey("dbo.ClassMembers", "class_id", "dbo.Lophocs", "class_id", cascadeDelete: true);
            AddForeignKey("dbo.Students", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Lophocs", "te_id", "dbo.Teachers", "te_id", cascadeDelete: true);
            AddForeignKey("dbo.Lichhocs", "te_id", "dbo.Teachers", "te_id", cascadeDelete: true);
            AddForeignKey("dbo.Khoahocs", "te_id", "dbo.Teachers", "te_id", cascadeDelete: true);
            AddForeignKey("dbo.Thongbaos", "Teacher_te_id", "dbo.Teachers", "te_id");
            AddForeignKey("dbo.Teachers", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
