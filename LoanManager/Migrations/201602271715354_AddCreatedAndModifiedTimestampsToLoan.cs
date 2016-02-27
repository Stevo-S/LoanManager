namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedAndModifiedTimestampsToLoan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Loans", "ModifiedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "ModifiedAt");
            DropColumn("dbo.Loans", "CreatedAt");
        }
    }
}
