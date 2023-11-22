using Microsoft.EntityFrameworkCore;
using SOLIDConsoleApp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.DataBase
{
    internal class CreditCardContext : DbContext
    {
        DbSet<ClientCreditCard> CreditCards => Set<ClientCreditCard>();
        private readonly CreditCardContext _clientCreditCards;

        CreditCardContext(CreditCardContext clientCreditCards)
        {
            this._clientCreditCards = clientCreditCards;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}
