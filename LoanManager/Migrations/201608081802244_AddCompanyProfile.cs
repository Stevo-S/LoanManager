namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyProfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Logo = c.String(maxLength: 256),
                        Name = c.String(maxLength: 256),
                        PostOfficeBox = c.String(maxLength: 10),
                        City = c.String(maxLength: 50),
                        ProvinceStateCounty = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CompanyProfiles");
        }
    }
}
