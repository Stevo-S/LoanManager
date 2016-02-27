namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaidFlagToDuePayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DuePayments", "IsPaid", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Borrowers", "MiddleName", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Borrowers", "MiddleName", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.DuePayments", "IsPaid");
        }
    }
}
