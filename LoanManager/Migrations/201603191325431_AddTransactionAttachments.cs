namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransactionAttachments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        TransactionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Transactions", t => t.TransactionId, cascadeDelete: true)
                .Index(t => t.TransactionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionAttachments", "TransactionId", "dbo.Transactions");
            DropIndex("dbo.TransactionAttachments", new[] { "TransactionId" });
            DropTable("dbo.TransactionAttachments");
        }
    }
}
