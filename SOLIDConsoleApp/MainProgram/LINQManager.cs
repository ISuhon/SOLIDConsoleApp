using SOLIDConsoleApp.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.MainProgram
{
    internal class LINQManager
    {
        BankContext? context;
        public LINQManager(BankContext? context) 
        {
            this.context = context;
        }
        internal void Union()
        {
            var ClientQuery = context?.Clients.Select(c => c.LastName);
            var BalanceQuery = context?.Balances.Select(b => b.Surname);

            var UnionQuery = ClientQuery.Union(BalanceQuery);

            foreach(var union in UnionQuery) 
            {
                Console.WriteLine(union);
            }
        }
    }
}
