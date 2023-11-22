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
        private readonly ClientContext _context;

        public ClientContext(ClientContext context)
        {
            this._context=context;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientData>()
                .Property(c => c.FirstName)
                .HasMaxLength(20)
                .IsRequired()
                .HasDefaultValue("Vasya");

            modelBuilder.Entity<ClientData>()
                .Property(c => c.MiddleName)
                .HasMaxLength(30)
                .HasDefaultValue("Popovich");

            modelBuilder.Entity<ClientData>()
                .Property(c => c.LastName)
                .HasMaxLength(50)
                .IsRequired();
        }

        

        //private readonly string _path = @"Server=DESKTOP-GN81J6L\SQLEXPRESS;
        //                                    Database=CodeFirstBank;
        //                                    Trusted_Connection=True;
        //                                    TrustServerCertificate=true;";

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(this._path);
        //}
    }
}
