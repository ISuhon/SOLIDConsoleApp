using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using SOLIDConsoleApp.Client;

namespace SOLIDConsoleApp.DataBase
{
    internal class ClientContext : DbContext
    {
        public DbSet<ClientData> Clients => Set<ClientData>();

        public ClientContext(DbContextOptions<ClientContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine(), new[] { RelationalEventId.CommandExecuted });
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

        
    }
}
