namespace CodeFisttExample.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CodeFisttExample.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<CodeFisttExample.Models.CustomerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CodeFisttExample.Models.CustomerDbContext context)
        {
            context.Customers.AddOrUpdate(x => x.CustomerId,
                new Customers() { LastName = "Carvajal", Name = "Hugo" },
                new Customers() { LastName = "Boeing", Name = "John" },
                new Customers() { LastName = "Wilson", Name = "Heather" }
                );


        }
    }
}
