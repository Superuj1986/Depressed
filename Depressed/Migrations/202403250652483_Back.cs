namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Lophocs", t => t.lophoc_id, cascadeDelete: false)
                .Index(t => t.lophoc_id);
            
            DropColumn("dbo.Lophocs", "Ngayhoc1");
            DropColumn("dbo.Lophocs", "Tiet_1");
            DropColumn("dbo.Lophocs", "Ngayhoc2");
            DropColumn("dbo.Lophocs", "Tiet_2");
            DropColumn("dbo.Lophocs", "Ngayhoc3");
            DropColumn("dbo.Lophocs", "Tiet_3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lophocs", "Tiet_3", c => c.String());
            AddColumn("dbo.Lophocs", "Ngayhoc3", c => c.DateTime());
            AddColumn("dbo.Lophocs", "Tiet_2", c => c.String());
            AddColumn("dbo.Lophocs", "Ngayhoc2", c => c.DateTime());
            AddColumn("dbo.Lophocs", "Tiet_1", c => c.String());
            AddColumn("dbo.Lophocs", "Ngayhoc1", c => c.DateTime());
            DropForeignKey("dbo.Lichhocs", "lophoc_id", "dbo.Lophocs");
            DropIndex("dbo.Lichhocs", new[] { "lophoc_id" });
            DropTable("dbo.Lichhocs");
        }
    }
}
