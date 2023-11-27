using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SOLIDConsoleApp.Client;

namespace SOLIDConsoleApp.DataBase
{
    internal class TransactionContext : DbContext
    {
        DbSet<Transaction> Transactions => Set<Transaction>();
        private string _connectionString;

        public TransactionContext() => Database.EnsureCreated();

        TransactionContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditData>()
                .Property(c => c.CreditSum)
                .HasMaxLength(10)
                .IsRequired();

            modelBuilder.Entity<CreditData>()
                .Property(c => c.CreditStatus)
                .HasDefaultValue(CreditStatus.ACTIVE);

            modelBuilder.Entity<CreditData>()
                .Property(c => c.CreditType)
                .HasDefaultValue(CreditType.REVOLVING_CREDIT);
        }
    }
}
