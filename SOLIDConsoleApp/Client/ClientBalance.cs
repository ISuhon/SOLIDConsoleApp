using SOLIDConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.Client
{
    internal class ClientBalance : SOLIDConsoleApp.Interfaces.IClientBalance
    {
        public IListofCreditCards? creditCards { get; set; }
        public string? Surname { get; set; }
        public ICredits? Credits { get; set; }
        public int BalanceID { get; set; } // Primary key
        public int ClientID { get; set; } // Foreign key

        public void setSurname(string? surname)
        {
            this.Surname = surname;
        }
        
    }
}
