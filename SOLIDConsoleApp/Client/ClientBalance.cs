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
        public int BalanceID { get; set; } // Primary key

        public void setSurname(string? surname)
        {
            this.Surname = surname;
        }
        
    }
}
