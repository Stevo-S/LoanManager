namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Details = c.String(),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Loans", t => t.LoanId, cascadeDelete: true)
                .ForeignKey("dbo.TransactionTypes", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.LoanId)
                .Index(t => t.TypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "TypeId", "dbo.TransactionTypes");
            DropForeignKey("dbo.Transactions", "LoanId", "dbo.Loans");
            DropIndex("dbo.Transactions", new[] { "TypeId" });
            DropIndex("dbo.Transactions", new[] { "LoanId" });
            DropTable("dbo.Transactions");
        }
    }
}
