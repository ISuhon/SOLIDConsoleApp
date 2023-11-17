using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SOLIDConsoleApp.Client;

namespace SOLIDConsoleApp.DataBase
{
    internal class Database : DbContext
    {
        public DbSet<ClientData> Clients => Set<ClientData>();
        public DbSet<ClientBalance> ClientsBalance => Set<ClientBalance>();
        public DbSet<ClientCreditCards> ClientsCreditCards => Set<ClientCreditCards>();
        public DbSet<Credits> Credits => Set<Credits>();
        public DbSet<TransactionHistory> Transactions => Set<TransactionHistory>();


        public Database() => Database.EnsureCreated();

        private readonly string _path = @"Server=localhost;Database=ConsoleBank;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._path);
        }
    }
}
