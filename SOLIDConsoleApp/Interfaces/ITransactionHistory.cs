using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.Interfaces
{
    internal interface ITransactionHistory
    {
        public List<ITransaction> GetTransactions();

        public ITransaction getTransaction(int id);

        public void addTransaction(ITransaction transaction);

        public void showAllTransactions();
    }
}
