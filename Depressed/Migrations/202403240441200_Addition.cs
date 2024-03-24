namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lichhocs", "Ngayhoc", c => c.DateTime());
            AddColumn("dbo.Lichhocs", "Tiet", c => c.String());
            DropColumn("dbo.Lichhocs", "Day1_Start");
            DropColumn("dbo.Lichhocs", "Day1_End");
            DropColumn("dbo.Lichhocs", "Day2_Start");
            DropColumn("dbo.Lichhocs", "Day2_End");
            DropColumn("dbo.Lichhocs", "Day3_Start");
            DropColumn("dbo.Lichhocs", "Day3_End");
            DropColumn("dbo.Lichhocs", "Day4_Start");
            DropColumn("dbo.Lichhocs", "Day4_End");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lichhocs", "Day4_End", c => c.DateTime());
            AddColumn("dbo.Lichhocs", "Day4_Start", c => c.DateTime());
            AddColumn("dbo.Lichhocs", "Day3_End", c => c.DateTime());
            AddColumn("dbo.Lichhocs", "Day3_Start", c => c.DateTime());
            AddColumn("dbo.Lichhocs", "Day2_End", c => c.DateTime());
            AddColumn("dbo.Lichhocs", "Day2_Start", c => c.DateTime());
            AddColumn("dbo.Lichhocs", "Day1_End", c => c.DateTime());
            AddColumn("dbo.Lichhocs", "Day1_Start", c => c.DateTime());
            DropColumn("dbo.Lichhocs", "Tiet");
            DropColumn("dbo.Lichhocs", "Ngayhoc");
        }
    }
}
