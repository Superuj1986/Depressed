namespace Depressed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Fullname", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Age", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Main_subject", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Address", c => c.Binary());
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Image");
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Main_subject");
            DropColumn("dbo.AspNetUsers", "Age");
            DropColumn("dbo.AspNetUsers", "Fullname");
        }
    }
}
