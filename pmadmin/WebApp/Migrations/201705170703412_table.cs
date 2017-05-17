namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkItemId = c.Int(nullable: false),
                        FileName = c.String(maxLength: 255),
                        Path = c.String(maxLength: 255),
                        Type = c.String(maxLength: 20),
                        Creator = c.String(maxLength: 20),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CheckLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        WorkItemId = c.Int(nullable: false),
                        Desc = c.String(maxLength: 255),
                        From = c.String(maxLength: 20),
                        To = c.String(maxLength: 20),
                        Status = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Discussions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        ParentId = c.Int(),
                        WorkItemId = c.Int(nullable: false),
                        Content = c.String(),
                        Tags = c.String(maxLength: 255),
                        Like = c.Int(nullable: false),
                        Creator = c.String(maxLength: 20),
                        AssignTo = c.String(maxLength: 20),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(maxLength: 20),
                        WorkItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Group = c.Int(nullable: false),
                        ExtensionKey1 = c.String(maxLength: 50),
                        Type = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        Content = c.String(),
                        ExtensionKey2 = c.String(maxLength: 50),
                        Tags = c.String(maxLength: 255),
                        StackTrace = c.String(),
                        Method = c.String(maxLength: 255),
                        User = c.String(maxLength: 20),
                        Created = c.DateTime(nullable: false),
                        IsNew = c.Int(nullable: false),
                        IsNotification = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Type = c.Int(nullable: false),
                        Message = c.String(maxLength: 255),
                        Link = c.String(maxLength: 255),
                        Status = c.Int(nullable: false),
                        To = c.String(maxLength: 20),
                        Creator = c.String(maxLength: 20),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkItemDependencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkItemId = c.Int(nullable: false),
                        DependentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.WorkItemId, t.DependentId }, unique: true, name: "Idx_Dependency");
            
            CreateTable(
                "dbo.WorkItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Desc = c.String(),
                        Tags = c.String(maxLength: 255),
                        Notes = c.String(maxLength: 255),
                        Priority = c.Int(nullable: false),
                        Effort = c.Int(nullable: false),
                        BusinessValue = c.Int(nullable: false),
                        TimeCriticality = c.Int(nullable: false),
                        Risk = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ClosedDate = c.DateTime(),
                        Owner = c.String(maxLength: 20),
                        Creator = c.String(maxLength: 20),
                        AssignTo = c.String(maxLength: 20),
                        TypeId = c.Int(nullable: false),
                        Visibility = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: false)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: false)
                .ForeignKey("dbo.WorkItemTypes", t => t.TypeId, cascadeDelete: false)
                .Index(t => t.TypeId)
                .Index(t => t.CompanyId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.WorkItemTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkItems", "TypeId", "dbo.WorkItemTypes");
            DropForeignKey("dbo.WorkItems", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.WorkItems", "CategoryId", "dbo.Categories");
            DropIndex("dbo.WorkItems", new[] { "CategoryId" });
            DropIndex("dbo.WorkItems", new[] { "CompanyId" });
            DropIndex("dbo.WorkItems", new[] { "TypeId" });
            DropIndex("dbo.WorkItemDependencies", "Idx_Dependency");
            DropTable("dbo.WorkItemTypes");
            DropTable("dbo.WorkItems");
            DropTable("dbo.WorkItemDependencies");
            DropTable("dbo.Notifications");
            DropTable("dbo.Messages");
            DropTable("dbo.Favorites");
            DropTable("dbo.Discussions");
            DropTable("dbo.CheckLists");
            DropTable("dbo.Attachments");
        }
    }
}
