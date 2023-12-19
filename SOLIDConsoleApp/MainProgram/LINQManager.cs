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

            PrintResult(UnionQuery);
        }

        internal void Except()
        {
            var CreditCardExpirationDateQuery = context?.CreditCards.Select(c => c.ExpirationDate);
            var CreditEndDateQuery = context?.Credits.Select(c => c.CreditEndDate);

            var ExceptQuery = CreditCardExpirationDateQuery.Except(CreditEndDateQuery);

            PrintResult(ExceptQuery);
        }

        internal void Intersect()
        {
            var TransactionsPayeeQuery = context?.Transactions.Select(t => t.Payee);
            var BalanceSurnameQuery = context?.Balances.Select(b => b.Surname);

            var IntersectQuery = TransactionsPayeeQuery.Intersect(BalanceSurnameQuery);

            PrintResult(IntersectQuery);
        }

        internal void Join()
        {
            var JoinQuery = from Balance in context?.Balances
                            join CreditCards in context?.CreditCards
                            on Balance.Id equals CreditCards.BalanceId
                            select new
                            {
                                Surname = Balance.Surname,
                                PIN = CreditCards.PIN
                            };

            foreach(var join in JoinQuery) 
            {
                Console.WriteLine($"{join.Surname} : {join.PIN}");
            }

        }

        private static void PrintResult(IQueryable queryable)
        {
            foreach(var result in queryable) 
            {
                Console.WriteLine(result);
            }
        }
    }
}
