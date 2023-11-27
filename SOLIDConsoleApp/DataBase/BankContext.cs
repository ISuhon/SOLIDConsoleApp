﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SOLIDConsoleApp.Client;
using SOLIDConsoleApp.Interfaces;

namespace SOLIDConsoleApp.DataBase
{
    internal class BankContext : DbContext
    {
        DbSet<ClientBalance> Balances => Set<ClientBalance>();
        DbSet<ClientData> Clients => Set<ClientData>();
        DbSet<ClientCreditCard> CreditCards => Set<ClientCreditCard>();
        DbSet<CreditData> Credits => Set<CreditData>();
        DbSet<Transaction> Transactions => Set<Transaction>();
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine(), new[] { RelationalEventId.CommandExecuted });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Client
            modelBuilder.Entity<ClientData>()
                .Property(c => c.FirstName)
                .HasMaxLength(20)
                .IsRequired(false);

            modelBuilder.Entity<ClientData>()
                .Property(c => c.MiddleName)
                .HasMaxLength(30)
                .HasDefaultValue("Popovich");

            modelBuilder.Entity<ClientData>()
                .Property(c => c.LastName)
                .HasMaxLength(50)
                .IsRequired();

            // Balance
            modelBuilder.Entity<ClientBalance>()
                .Property(c => c.Surname)
                .HasMaxLength(50)
                .IsRequired();

            // Credit card
            modelBuilder.Entity<ClientCreditCard>()
                .Property(c => c.CardNumber)
                .HasMaxLength(16)
                .IsRequired()
                .HasDefaultValue("6666666666666666");

            modelBuilder.Entity<ClientCreditCard>()
                .Property(c => c.CVVcode)
                .HasMaxLength(3)
                .IsRequired()
                .HasDefaultValue(555);

            modelBuilder.Entity<ClientCreditCard>()
                .Property(c => c.Fortune)
                .IsRequired(false);

            modelBuilder.Entity<ClientCreditCard>()
                .Property(c => c.PIN)
                .HasMaxLength(4)
                .IsRequired()
                .HasDefaultValue(1337);

            // Credit
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
