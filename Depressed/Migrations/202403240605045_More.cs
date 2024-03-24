namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class More : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lichhocs", "Ngayhoc1", c => c.DateTime());
            AddColumn("dbo.Lichhocs", "Tiet_1", c => c.String());
            AddColumn("dbo.Lichhocs", "Ngayhoc2", c => c.DateTime());
            AddColumn("dbo.Lichhocs", "Tiet_2", c => c.String());
            AddColumn("dbo.Lichhocs", "Ngayhoc3", c => c.DateTime());
            AddColumn("dbo.Lichhocs", "Tiet_3", c => c.String());
            DropColumn("dbo.Lichhocs", "Ngayhoc");
            DropColumn("dbo.Lichhocs", "Tiet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lichhocs", "Tiet", c => c.String());
            AddColumn("dbo.Lichhocs", "Ngayhoc", c => c.DateTime());
            DropColumn("dbo.Lichhocs", "Tiet_3");
            DropColumn("dbo.Lichhocs", "Ngayhoc3");
            DropColumn("dbo.Lichhocs", "Tiet_2");
            DropColumn("dbo.Lichhocs", "Ngayhoc2");
            DropColumn("dbo.Lichhocs", "Tiet_1");
            DropColumn("dbo.Lichhocs", "Ngayhoc1");
        }
    }
}
