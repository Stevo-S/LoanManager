namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDuePayments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DuePayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DueDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Loans", t => t.LoanId, cascadeDelete: true)
                .Index(t => t.LoanId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DuePayments", "LoanId", "dbo.Loans");
            DropIndex("dbo.DuePayments", new[] { "LoanId" });
            DropTable("dbo.DuePayments");
        }
    }
}
