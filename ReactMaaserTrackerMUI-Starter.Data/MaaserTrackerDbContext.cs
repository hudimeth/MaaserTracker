using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ReactMaaserTrackerMUI_Starter.Data
{
    public class MaaserTrackerDbContext :DbContext
    {
        private string _connectionString;
        public MaaserTrackerDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IncomeSource>()
                .HasMany(s => s.Incomes)
                .WithOne(i => i.Source)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MaaserPayment>()
                .ToTable("MaaserPayments");
        }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<MaaserPayment> MaaserPayments { get; set; }
        public DbSet<IncomeSource> Sources { get; set; }
    }
}
