namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkItemId = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 10),
                        Desc = c.String(maxLength: 255),
                        Notes = c.String(maxLength: 255),
                        TransactionDateTime = c.DateTime(nullable: false),
                        Operator = c.String(maxLength: 20),
                        PublicKey = c.String(maxLength: 50),
                        Event = c.String(maxLength: 30),
                        TableName = c.String(maxLength: 30),
                        Flag = c.Int(nullable: false),
                        Creator = c.String(maxLength: 20),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TransactionHistories");
        }
    }
}
