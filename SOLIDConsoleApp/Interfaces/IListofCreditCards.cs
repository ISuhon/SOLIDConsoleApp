using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.Interfaces
{
    internal interface IListofCreditCards
    {
        public List<ICreditCard> getCreditCards();
        public void addCreditCard(ICreditCard creditCard);
        public void showCreditCards();
    }
}
