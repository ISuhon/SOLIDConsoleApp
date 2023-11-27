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
        public IListofCreditCards? creditCards { get; set; }
        public string? Surname { get; set; }

        [NotMapped]
        public ICredits? Credits { get; set; }

        [Key]
        [ForeignKey("ClientData")]
        public int BalanceID { get; set; } // Primary key

        [NotMapped]
        public IClientData? ClientData { get; set; }

        public ICollection<ICreditCard>? CreditCardsForDB { get; set; }
        public ICollection<ICreditData>? CreditsForDB { get; set; }

        public ClientBalance()
        {
            this.CreditCardsForDB = creditCards?.getCreditCards();
            this.CreditsForDB = Credits?.getCredits();
        }

        public void setSurname(string? surname)
        {
            this.Surname = surname;
        }

        public override string ToString()
        {
            return $"ID : {this.BalanceID}\n" +
                $"Surname : {this.Surname}\n" +
                $"\n========================\n";
        }
    }
}
