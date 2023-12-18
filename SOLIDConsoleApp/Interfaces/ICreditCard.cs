using SOLIDConsoleApp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.Interfaces
{
    public interface ICreditCard
    {
        public string? CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CVVcode { get; set; }
        public int PIN { get; set; }
        public double Fortune { get; set; }
        public ITransactionHistory? transactions { get; set; }
    }
}
