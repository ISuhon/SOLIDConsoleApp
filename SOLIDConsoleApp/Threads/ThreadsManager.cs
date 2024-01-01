using Microsoft.EntityFrameworkCore.Internal;
using SOLIDConsoleApp.Client;
using SOLIDConsoleApp.DataBase.ContextFactory;
using SOLIDConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.Threads
{
    internal class ThreadsManager
    {
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private static BankContextFactory contextFactory = new BankContextFactory();

        static void CreateAndSaveClient()
        {
            Random rand = new Random();

            // Adding client to DB
            try
            {
                semaphore.Wait();

                using (var context = contextFactory.CreateDbContext(new string[] { }))
                {
                    
                    ClientData client1 = new ClientData
                    {
                        FirstName = GenerateUniqueString(0, 10),
                        MiddleName = GenerateUniqueString(0, 30),
                        LastName = GenerateUniqueString(0, 30),
                        PhoneNumber = GenerateUniqueStringNumber(),
                        Email = (GenerateUniqueString(0, 15) + "@gmail.com").ToLower()
                    };

                    context.Clients.Add(client1);
                    context.SaveChanges();

                    Console.WriteLine($"Client {client1.LastName} {client1.FirstName} {client1.MiddleName} saved.");
                }
            } finally 
            { 
                semaphore.Release(); 
            }

            // Adding balance to DB
            try
            {
                semaphore.Wait();

                using (var context = contextFactory.CreateDbContext(new string[] { }))
                {
                    ClientBalance balance1 = new ClientBalance();

                    balance1.Surname = context.Clients.Single().LastName;
                    balance1.ClientData = context.Clients.Single();

                    context.Balances.Add(balance1);
                    context.SaveChanges();

                    Console.WriteLine($"Balance of {balance1.Surname} saved.");
                }
            }
            finally
            {
                semaphore.Release();
            }

            // Adding credit card to DB
            try
            {
                semaphore.Wait();

                using (var context = contextFactory.CreateDbContext(new string[] { }))
                {
                    ClientCreditCard creditCard1 = new ClientCreditCard()
                    {
                        CardNumber = GenerateUniqueString(0, 15),
                        CVVcode = rand.Next(100, 999),
                        PIN = rand.Next(1000, 9999),
                        ExpirationDate = DateTime.Now,
                        Fortune = 0.0,
                        Balance = context.Balances.Single(),
                    };

                    context.CreditCards.Add(creditCard1);
                    context.SaveChanges();

                    Console.WriteLine($"Credit card {creditCard1.CardNumber} saved");
                }
            }
            finally
            {
                semaphore.Release();
            }

            // Adding credit to DB
            try
            {
                semaphore.Wait();

                using (var context = contextFactory.CreateDbContext(new string[] { }))
                {

                    CreditData credit1 = new CreditData((rand.NextDouble() + 0.5) * rand.Next(500, 100000),
                        CreditType.OPEN_CREDIT, CreditStatus.ACTIVE, DateTime.Now)
                    {
                        Balance = context.Balances.Single()
                    };

                    context.Credits.Add(credit1);
                    context.SaveChanges();

                    Console.WriteLine($"Credit saved.");
                }
            }
            finally
            {
                semaphore.Release();
            }

            // Adding transaction to DB
            try
            {
                semaphore.Wait();

                using (var context = contextFactory.CreateDbContext(new string[] { }))
                {
                    Transaction transaction1 = new Transaction(context.Clients.Single().FirstName + " " + context.Clients.Single().LastName, context.Clients.Single().Id, DateTime.Now, 2500) { ClientCreditCard = context.CreditCards.Single() };

                    context.Transactions.Add(transaction1);
                    context.SaveChanges();

                    Console.WriteLine($"Transaction of {transaction1.Payee} saved.");
                }
            }
            finally
            {
                semaphore.Release();
            }
        }

        static string GenerateUniqueString(int startIndex, int length)
        {
            return Guid.NewGuid().ToString().Substring(startIndex, length);
        }

        static string GenerateUniqueStringNumber()
        {
            return "+380" + Guid.NewGuid().ToString().Substring(0, 8);
        }
        static string GenerateUniqueStringNumber(string countryCode)
        {
            return countryCode + Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
