using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SOLIDConsoleApp.Client;
using SOLIDConsoleApp.Interfaces;

namespace SOLIDConsoleApp.DataBase
{
    internal class BalanceContext : DbContext
    {
        DbSet<ClientBalance> Balances => Set<ClientBalance>();
        private readonly ClientBalance _balance;

        public BalanceContext(ClientBalance balance)
        {
            this._balance=balance;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientBalance>()
                .Property(c => c.Surname)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
