namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInstallmentsCountToLoan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "InitialInstallments", c => c.Int(nullable: false));
            AddColumn("dbo.Loans", "PendingInstallments", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "PendingInstallments");
            DropColumn("dbo.Loans", "InitialInstallments");
        }
    }
}
