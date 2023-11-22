﻿using System;
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
        private readonly TransactionContext _transactions;

        public TransactionContext(TransactionContext transactions)
        {
            this._transactions=transactions;
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
