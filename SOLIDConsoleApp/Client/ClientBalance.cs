using SOLIDConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOLIDConsoleApp.Client
{
    internal class ClientBalance : SOLIDConsoleApp.Interfaces.IClientBalance
    {
        [NotMapped]
        public List<ICreditCard>? creditCards { get; set; }
        public string? Surname { get; set; }

        [NotMapped]
        public ICredits? Credits { get; set; }

        [Key]
        public int BalanceID { get; set; } // Primary key

        public void setSurname(string? surname)
        {
            this.Surname = surname;
        }

        public List<ICreditCard>? getCreditCards()
        {
            return this.creditCards;
        }
        public void addCreditCard(ICreditCard creditCard)
        {
            this.creditCards?.Add(creditCard);
        }
        public void showCreditCards() 
        {
            foreach(ICreditCard? creditCard in this.creditCards)
            {
                Console.WriteLine(creditCard);
            }
        }

        public override string ToString()
        {
            return $"ID : {this.BalanceID}\n" +
                $"Surname : {this.Surname}\n" +
                $"\n========================\n";
        }
    }
}
