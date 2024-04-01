namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WTF : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lichhocs", "lophoc_id", "dbo.Lophocs");
            DropForeignKey("dbo.RollOuts", "lh_id", "dbo.Lichhocs");
            DropIndex("dbo.Lichhocs", new[] { "lophoc_id" });
            DropIndex("dbo.RollOuts", new[] { "lh_id" });
            AddColumn("dbo.Lophocs", "Ngayhoc1", c => c.DateTime());
            AddColumn("dbo.Lophocs", "Tiet_1", c => c.String());
            AddColumn("dbo.Lophocs", "Ngayhoc2", c => c.DateTime());
            AddColumn("dbo.Lophocs", "Tiet_2", c => c.String());
            AddColumn("dbo.Lophocs", "Ngayhoc3", c => c.DateTime());
            AddColumn("dbo.Lophocs", "Tiet_3", c => c.String());
            DropColumn("dbo.RollOuts", "lh_id");
            DropTable("dbo.Lichhocs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Lichhocs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        lophoc_id = c.Int(nullable: false),
                        Ngayhoc1 = c.DateTime(),
                        Tiet_1 = c.String(),
                        Ngayhoc2 = c.DateTime(),
                        Tiet_2 = c.String(),
                        Ngayhoc3 = c.DateTime(),
                        Tiet_3 = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.RollOuts", "lh_id", c => c.Int(nullable: false));
            DropColumn("dbo.Lophocs", "Tiet_3");
            DropColumn("dbo.Lophocs", "Ngayhoc3");
            DropColumn("dbo.Lophocs", "Tiet_2");
            DropColumn("dbo.Lophocs", "Ngayhoc2");
            DropColumn("dbo.Lophocs", "Tiet_1");
            DropColumn("dbo.Lophocs", "Ngayhoc1");
            CreateIndex("dbo.RollOuts", "lh_id");
            CreateIndex("dbo.Lichhocs", "lophoc_id");
            AddForeignKey("dbo.RollOuts", "lh_id", "dbo.Lichhocs", "id", cascadeDelete: true);
            AddForeignKey("dbo.Lichhocs", "lophoc_id", "dbo.Lophocs", "class_id", cascadeDelete: true);
        }
    }
}
