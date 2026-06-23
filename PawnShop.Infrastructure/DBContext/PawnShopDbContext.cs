using Microsoft.EntityFrameworkCore;
using PawnShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Infrastructure.DBContext
{
    public class PawnShopDbContext : DbContext
    {
        public PawnShopDbContext(
            DbContextOptions<PawnShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tenant> Tenants => Set<Tenant>();

        public DbSet<User> Users => Set<User>();

        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<Loan> Loans => Set<Loan>();

        public DbSet<GoldItem> GoldItems => Set<GoldItem>();

        public DbSet<Payment> Payments => Set<Payment>();

        public DbSet<CapitalContributor> CapitalContributors => Set<CapitalContributor>();

        public DbSet<CapitalTransaction> CapitalTransactions => Set<CapitalTransaction>();

        public DbSet<ProfitTransaction> ProfitTransactions => Set<ProfitTransaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Loan>()
    .HasOne(x => x.Customer)
    .WithMany(x => x.Loans)
    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
