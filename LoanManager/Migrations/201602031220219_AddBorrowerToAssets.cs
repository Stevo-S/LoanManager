namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBorrowerToAssets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "BorrowerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Assets", "BorrowerId");
            AddForeignKey("dbo.Assets", "BorrowerId", "dbo.Borrowers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assets", "BorrowerId", "dbo.Borrowers");
            DropIndex("dbo.Assets", new[] { "BorrowerId" });
            DropColumn("dbo.Assets", "BorrowerId");
        }
    }
}
