using Microsoft.EntityFrameworkCore.Internal;
using SOLIDConsoleApp.Client;
using SOLIDConsoleApp.DataBase.ContextFactory;
using SOLIDConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.Threads
{
    internal class ThreadsManager
    {
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private static readonly object lockObject = new object();
        private static BankContextFactory contextFactory = new BankContextFactory();

        internal static void CreateAndSaveClient()
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
                        FirstName = WriteName(),
                        MiddleName = WriteName(),
                        LastName = WriteName(),
                        PhoneNumber = GenerateUniqueStringNumber(),
                        Email = (WriteName() + "@gmail.com").ToLower()
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
                        CardNumber = WriteName(),
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

        internal static async Task AsyncCreateAndSaveClient()
        {
            Random rand = new Random();

            using (var context = contextFactory.CreateDbContext(new string[] { }))
            {
                await Task.Delay(500);

                lock (lockObject)
                {
                    // Adding client to DB
                    ClientData client1 = new ClientData
                    {
                        FirstName = WriteName(),
                        MiddleName = WriteName(),
                        LastName = WriteName(),
                        PhoneNumber = GenerateUniqueStringNumber("+" + rand.Next(101, 998)),
                        Email = (WriteName() + "@gmail.com").ToLower()
                    };

                    context.Clients.Add(client1);
                    context.SaveChanges();

                    Console.WriteLine($"Client {client1.LastName} {client1.FirstName} {client1.MiddleName} saved.");

                    // Adding balance to DB
                    Task.Delay(500).Wait();

                    ClientBalance balance1 = new ClientBalance();

                    balance1.Surname = context.Clients.Single().LastName;
                    balance1.ClientData = context.Clients.Single();

                    context.Balances.Add(balance1);
                    context.SaveChanges();

                    Console.WriteLine($"Balance of {balance1.Surname} saved.");

                    // Adding credit card to DB
                    Task.Delay(500).Wait();

                    ClientCreditCard creditCard1 = new ClientCreditCard()
                    {
                        CardNumber = WriteName(),
                        CVVcode = rand.Next(100, 999),
                        PIN = rand.Next(1000, 9999),
                        ExpirationDate = DateTime.Now,
                        Fortune = 0.0,
                        Balance = context.Balances.Single(),
                    };

                    context.CreditCards.Add(creditCard1);
                    context.SaveChanges();

                    Console.WriteLine($"Credit card {creditCard1.CardNumber} saved");

                    // Adding credit to DB
                    Task.Delay(500).Wait();

                    CreditData credit1 = new CreditData((rand.NextDouble() + 0.5) * rand.Next(500, 100000),
                        CreditType.OPEN_CREDIT, CreditStatus.ACTIVE, DateTime.Now)
                    {
                        Balance = context.Balances.Single()
                    };

                    context.Credits.Add(credit1);
                    context.SaveChanges();

                    Console.WriteLine($"Credit saved.");

                    // Adding transaction to DB
                    Task.Delay(500).Wait();

                    Transaction transaction1 = new Transaction(context.Clients.Single().FirstName + " " + context.Clients.Single().LastName, context.Clients.Single().Id, DateTime.Now, 2500) { ClientCreditCard = context.CreditCards.Single() };

                    context.Transactions.Add(transaction1);
                    context.SaveChanges();

                    Console.WriteLine($"Transaction of {transaction1.Payee} saved.");
                }
            }
                
            
        }
        static string WriteName()
        {
#nullable disable
            return Console.ReadLine();
#nullable enable
        }

        static string GenerateUniqueStringNumber()
        {
            return "+380" + BigInteger.Parse(Guid.NewGuid().GetHashCode().ToString().Substring(0, 8), System.Globalization.NumberStyles.AllowDecimalPoint);
        }
        static string GenerateUniqueStringNumber(string countryCode)
        {
            return countryCode + BigInteger.Parse(Guid.NewGuid().GetHashCode().ToString().Substring(0, 8), System.Globalization.NumberStyles.AllowDecimalPoint);
        }
    }
}
