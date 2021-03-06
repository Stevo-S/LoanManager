namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSloganToCompanyProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyProfiles", "Slogan", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyProfiles", "Slogan");
        }
    }
}
