using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CodeFisttExample.Models
{
    public class CustomerDbContext : DbContext
    {
        
        public CustomerDbContext() : base("name=CustomerContext")
        {
        }


        public DbSet<Customers> Customers { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Expenses> Expenses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Customers>()
                        .HasMany(e => e.Projects)
                        .WithRequired(e => e.Customers)
                        .HasForeignKey(e => e.CustomerId)
                        .WillCascadeOnDelete(false);     


            modelBuilder.Entity<Customers>().MapToStoredProcedures();
            modelBuilder.Entity<Projects>().MapToStoredProcedures();
            modelBuilder.Entity<Expenses>().MapToStoredProcedures();

            


        }
    }
}