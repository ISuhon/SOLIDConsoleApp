using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SOLIDConsoleApp.Client;

namespace SOLIDConsoleApp.DataBase
{
    internal class ClientContext : DbContext
    {
        public DbSet<ClientData> Clients => Set<ClientData>();

        public ClientContext() => Database.EnsureCreated();

        private readonly string _path = @"Server=DESKTOP-GN81J6L\SQLEXPRESS;
                                            Database=CodeFirstBank;
                                            Trusted_Connection=True;
                                            TrustServerCertificate=true;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._path);
        }
    }
}
