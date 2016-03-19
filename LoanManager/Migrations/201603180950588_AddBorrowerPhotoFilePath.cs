namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBorrowerPhotoFilePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Borrowers", "Photo", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Borrowers", "Photo");
        }
    }
}
