namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDateColumnFromLoan : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Loans", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Loans", "Date", c => c.DateTime(nullable: false));
        }
    }
}
