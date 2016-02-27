namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataAnnotationsAndUpdateAccountViewModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "RegistrationNo", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.Assets", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Assets", "LogBookId", c => c.String(nullable: false));
            AlterColumn("dbo.Borrowers", "NationalID", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Borrowers", "FirstName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Borrowers", "MiddleName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Borrowers", "LastName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Borrowers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Borrowers", "PhoneNumber1", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.Borrowers", "PhoneNumber2", c => c.String(maxLength: 12));
            AlterColumn("dbo.Borrowers", "Address", c => c.String(maxLength: 255));
            AlterColumn("dbo.Transactions", "Details", c => c.String(nullable: false));
            AlterColumn("dbo.Transactions", "Credit", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Transactions", "Debit", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TransactionTypes", "Description", c => c.String(nullable: false));
            CreateIndex("dbo.Borrowers", "NationalID", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Borrowers", new[] { "NationalID" });
            AlterColumn("dbo.TransactionTypes", "Description", c => c.String());
            AlterColumn("dbo.Transactions", "Debit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Transactions", "Credit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Transactions", "Details", c => c.String());
            AlterColumn("dbo.Borrowers", "Address", c => c.String());
            AlterColumn("dbo.Borrowers", "PhoneNumber2", c => c.String());
            AlterColumn("dbo.Borrowers", "PhoneNumber1", c => c.String());
            AlterColumn("dbo.Borrowers", "Email", c => c.String());
            AlterColumn("dbo.Borrowers", "LastName", c => c.String());
            AlterColumn("dbo.Borrowers", "MiddleName", c => c.String());
            AlterColumn("dbo.Borrowers", "FirstName", c => c.String());
            AlterColumn("dbo.Borrowers", "NationalID", c => c.String());
            AlterColumn("dbo.Assets", "LogBookId", c => c.String());
            AlterColumn("dbo.Assets", "Description", c => c.String());
            DropColumn("dbo.Assets", "RegistrationNo");
        }
    }
}
