namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Principal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Interest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Cleared = c.Boolean(nullable: false),
                        AssetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assets", t => t.AssetId, cascadeDelete: true)
                .Index(t => t.AssetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "AssetId", "dbo.Assets");
            DropIndex("dbo.Loans", new[] { "AssetId" });
            DropTable("dbo.Loans");
        }
    }
}
