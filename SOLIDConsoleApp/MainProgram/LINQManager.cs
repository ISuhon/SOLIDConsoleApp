using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SOLIDConsoleApp.DataBase;
using SOLIDConsoleApp.Client;
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
#nullable disable
        internal void Union()
        {
            var clientQuery = context?.Clients.Select(c => c.LastName);
            var balanceQuery = context?.Balances.Select(b => b.Surname);

            if(CheckNullReference(clientQuery))
            {
                Console.WriteLine("Client is empty");
                return;
            }

            if (CheckNullReference(balanceQuery))
            {
                Console.WriteLine("Balance is empty");
                return;
            }

            var unionQuery = clientQuery.Union(balanceQuery);

            PrintResult(unionQuery);
        }

        internal void Except()
        {
            var creditCardExpirationDateQuery = context?.CreditCards.Select(c => c.ExpirationDate);
            var creditEndDateQuery = context?.Credits.Select(c => c.CreditEndDate);

            if (CheckNullReference(creditCardExpirationDateQuery))
            {
                Console.WriteLine("Expiration date of credit cards is empty");
                return;
            }

            if (CheckNullReference(creditEndDateQuery))
            {
                Console.WriteLine("End date of credit is empty");
                return;
            }

            var exceptQuery = creditCardExpirationDateQuery.Except(creditEndDateQuery);

            PrintResult(exceptQuery);
        }

        internal void Intersect()
        {
            var transactionsPayeeQuery = context?.Transactions.Select(t => t.Payee);
            var balanceSurnameQuery = context?.Balances.Select(b => b.Surname);

            if (CheckNullReference(transactionsPayeeQuery))
            {
                Console.WriteLine("Transactions payee is empty");
                return;
            }

            if (CheckNullReference(balanceSurnameQuery))
            {
                Console.WriteLine("Balance surname is empty");
                return;
            }

            var intersectQuery = transactionsPayeeQuery.Intersect(balanceSurnameQuery);

            PrintResult(intersectQuery);
        }

        internal void Join()
        {
            var joinQuery = from Balance in context?.Balances
                            join CreditCards in context?.CreditCards
                            on Balance.Id equals CreditCards.BalanceId
                            select new
                            {
                                Surname = Balance.Surname,
                                PIN = CreditCards.PIN
                            };

            if (CheckNullReference(joinQuery))
            {
                Console.WriteLine("Can't find any references");
                return;
            }

            foreach (var join in joinQuery) 
            {
                Console.WriteLine($"{join.Surname} : {join.PIN}");
            }

        }

        internal void Distinct()
        {
            var transactionsAmountDistinctQuery = context?.Transactions.Select(t => t.TransactionAmount).Distinct();

            if (CheckNullReference(transactionsAmountDistinctQuery))
            {
                Console.WriteLine("Transactions amount is empty");
                return;
            }

            PrintResult(transactionsAmountDistinctQuery);
        }

        internal void GroupBy()
        {
            var groupedQuery = context?.Credits
                .GroupBy(c => c.CreditStatus)
                .Select(group => new
                {
                    Value = group.Key,
                    Count = group.Count()
                });

            foreach (var group in groupedQuery)
            {
                Console.WriteLine($"Value : {group.Value} \nCount : {group.Count}");
            }
        }

        internal void AggregateFunctions()
        {
            // Aggregate function : counting number of transactions
            var count = context?.Transactions.Count();
            Console.WriteLine($"Total transactions : {count}");

            // Aggregate function : finding minimal amount of transaction
            var minimalAmount = context?.Transactions.Min(t => t.TransactionAmount);
            Console.WriteLine($"Minimal transaction amount : {minimalAmount}");
        }

        internal void EagerLoading()
        {
            var balancesWithCredits = context?.Balances.Include(c => c.CreditCardsForDB);

            if (CheckNullReference(balancesWithCredits))
            {
                Console.WriteLine("There is no such balances");
                return;
            }

            PrintResult(balancesWithCredits);
        }

        internal void ExplicitLoading()
        {
            var balances = context?.Balances.FirstOrDefault();

            if (CheckNullReference(balances))
            {
                Console.WriteLine("Balances is empty");
                return;
            }

            context?.Entry(balances).Collection(c => c.CreditsForDB).Load();

            Console.WriteLine($"Balance: {balances.Surname}");
            PrintResult(balances.CreditsForDB);
        }

        internal void LazyLoading()
        {
            var balances = context?.Balances?.FirstOrDefault();

            if(CheckNullReference(balances.CreditsForDB))
            {
                Console.WriteLine("Balance is null");
                return;
            }

            PrintResult(balances.CreditsForDB);
        }

        internal void NoTrackingData()
        {
            var untrackedTransaction = context?.Transactions.AsNoTracking().ToList();

            untrackedTransaction[0].TransactionAmount = 2500;

            context.Transactions.Attach(untrackedTransaction[0]);
            context.Entry(untrackedTransaction[0]).State = EntityState.Modified;

            context.SaveChanges();

            PrintResult(context?.Transactions);
        }

        internal void StoredProcedure()
        {
            var result = context?.Credits.FromSqlRaw("EXEC dbo.GetCreditsBySumAndStatus");

            if(CheckNullReference(result))
            {
                Console.WriteLine("Didn't found any credit");
                return;
            }

            PrintResult(result);
        }

        private static void PrintResult<T>(IEnumerable<T> queryable)
        {
            foreach(var result in queryable) 
            {
                Console.WriteLine(result);
            }
        }
#nullable enable

        private static bool CheckNullReference<T>(T obj)
        {
            return (obj == null) ? true : false;
        }
    }
}
