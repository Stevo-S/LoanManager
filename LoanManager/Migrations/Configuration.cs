namespace LoanManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LoanManager.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LoanManager.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.TransactionTypes.AddOrUpdate(
                t => t.Description,
                new Models.TransactionType() { Description = "OPENING BALANCE" },
                new Models.TransactionType() { Description = "PAYMENT" },
                new Models.TransactionType() { Description = "PENALTY" }
            );

            // Seed with default company profile
            if (!context.CompanyProfiles.Any())
            {
                context.CompanyProfiles.AddOrUpdate(c => c.Name,
                    new Models.CompanyProfile()
                    {
                        Name = "The Company",
                        City = "Nairobi",
                        ProvinceStateCounty = "Nairobi",
                        PostOfficeBox = "12345",
                        Country = "Kenya"
                    }
                    );
            }

        }
    }
}
