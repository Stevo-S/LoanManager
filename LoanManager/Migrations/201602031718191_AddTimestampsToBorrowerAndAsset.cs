namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimestampsToBorrowerAndAsset : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Assets", "ModifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Borrowers", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Borrowers", "ModifiedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Borrowers", "ModifiedAt");
            DropColumn("dbo.Borrowers", "CreatedAt");
            DropColumn("dbo.Assets", "ModifiedAt");
            DropColumn("dbo.Assets", "CreatedAt");
        }
    }
}
