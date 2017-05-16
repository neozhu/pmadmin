namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Desc", c => c.String(maxLength: 255));
            AddColumn("dbo.Categories", "CompanyId", c => c.Int(nullable: false));
            AddColumn("dbo.Companies", "Logo", c => c.String(maxLength: 20));
            AddColumn("dbo.Companies", "Domain", c => c.String(maxLength: 255));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Companies", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Companies", "Address", c => c.String(maxLength: 255));
            AlterColumn("dbo.Companies", "City", c => c.String(maxLength: 50));
            AlterColumn("dbo.Companies", "Province", c => c.String(maxLength: 50));
            CreateIndex("dbo.Categories", "CompanyId");
            AddForeignKey("dbo.Categories", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
            DropColumn("dbo.Companies", "Employees");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "Employees", c => c.Int(nullable: false));
            DropForeignKey("dbo.Categories", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Categories", new[] { "CompanyId" });
            AlterColumn("dbo.Companies", "Province", c => c.String());
            AlterColumn("dbo.Companies", "City", c => c.String());
            AlterColumn("dbo.Companies", "Address", c => c.String());
            AlterColumn("dbo.Companies", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Companies", "Domain");
            DropColumn("dbo.Companies", "Logo");
            DropColumn("dbo.Categories", "CompanyId");
            DropColumn("dbo.Categories", "Desc");
        }
    }
}
