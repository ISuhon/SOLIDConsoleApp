using SOLIDConsoleApp.Client;
using SOLIDConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SOLIDConsoleApp.MainProgram.EmployeeOptions;
using static SOLIDConsoleApp.MainProgram.ClientOptions;
using SOLIDConsoleApp.DataBase;
using SOLIDConsoleApp.DataBase.ContextFactory;

namespace SOLIDConsoleApp.MainProgram
{
    internal class Program
    {
        internal static List<IClientData> _clients = new List<IClientData>();

       

        public static void Main(string[] args)
        {
            var contextFactory = new BankContextFactory();

            using(var context = contextFactory.CreateDbContext(new string[] { }))
            {
                LINQManager manager = new LINQManager(context);

                manager.EagerLoading();
            }
            
        }



        private static void AddedToDB(BankContext? context)
        {
            // Adding client to DB
            ClientData client1 = new ClientData
            {
                FirstName = "Dmitriy",
                MiddleName = "Yaroslavovich",
                LastName = "Koshik",
                PhoneNumber = "+380678546275",
                Email = "YaDima@gmail.com".ToLower()
            };
            ClientData client2 = new ClientData
            {
                FirstName = "Oleksandr",
                MiddleName = "Valeriyovich",
                LastName = "Matosha",
                PhoneNumber = "+380984746575",
                Email = "VaOlek@gmail.com".ToLower()
            };
            context.Clients.AddRange(client1, client2);
            context.SaveChanges();

            // Adding balance of client to DB
            ClientBalance balance1 = new ClientBalance();
            ClientBalance balance2 = new ClientBalance();

            balance1.Surname = client1.LastName;
            balance1.ClientData = client1;

            balance2.Surname = client2.LastName;
            balance2.ClientData = client2;

            context.Balances.AddRange(balance1, balance2);
            context.SaveChanges();

            // Adding client card of client to DB
            ClientCreditCard creditCard1 = new ClientCreditCard() { CardNumber = "4562895645264753", CVVcode = 735, PIN = 6374, ExpirationDate = DateTime.Now, Fortune = 0.0, Balance = balance1 };
            ClientCreditCard creditCard2 = new ClientCreditCard() { CardNumber = "4557325123568213", CVVcode = 594, PIN = 6492, ExpirationDate = DateTime.Now, Fortune = 0.0, Balance = balance1 };
            ClientCreditCard creditCard3 = new ClientCreditCard() { CardNumber = "4214562345124678", CVVcode = 953, PIN = 3226, ExpirationDate = DateTime.Now, Fortune = 0.0, Balance = balance2 };

            context.CreditCards.AddRange(creditCard1, creditCard2, creditCard3);
            context.SaveChanges();

            // Adding credits of client to DB
            CreditData credit1 = new CreditData(150_000_000.0, CreditType.OPEN_CREDIT, CreditStatus.ACTIVE, DateTime.Now) { Balance = balance1 };
            CreditData credit2 = new CreditData(300_000_000.0, CreditType.INSTALLMENT_CREDIT, CreditStatus.ACTIVE, DateTime.Now) { Balance = balance1 };
            CreditData credit3 = new CreditData(450_000_000.0, CreditType.OPEN_CREDIT, CreditStatus.OVERDUE, DateTime.Now) { Balance = balance2 };
            CreditData credit4 = new CreditData(600_000_000.0, CreditType.REVOLVING_CREDIT, CreditStatus.ACTIVE, DateTime.Now) { Balance = balance1 };
            CreditData credit5 = new CreditData(850_000_000.0, CreditType.INSTALLMENT_CREDIT, CreditStatus.CLOSED, DateTime.Now) { Balance = balance2 };

            context.Credits.AddRange(new List<CreditData> { credit1, credit2, credit3, credit4, credit5 });
            context.SaveChanges();

            // Adding transactions of client to DB
            Transaction transaction1 = new Transaction(client2.FirstName + " " + client2.LastName, client1.Id, DateTime.Now, 2500) { ClientCreditCard = creditCard1 };
            Transaction transaction2 = new Transaction(client2.FirstName + " " + client2.LastName, client1.Id, DateTime.Now, 1000) { ClientCreditCard = creditCard2 };
            Transaction transaction3 = new Transaction(client1.FirstName + " " + client1.LastName, client2.Id, DateTime.Now, 15000) { ClientCreditCard = creditCard3 };
            Transaction transaction4 = new Transaction(client1.FirstName + " " + client1.LastName, client2.Id, DateTime.Now, 800) { ClientCreditCard = creditCard3 };

            context.Transactions.AddRange(new List<Transaction> { transaction1, transaction2, transaction3, transaction4 });
            context.SaveChanges();
        }

        private static void UpdateCreditStatus(BankContext? context)
        {
            int creditIdToUpdate = 1; // Припустиме значення Id кредиту, яке ви хочете оновити

            var creditToUpdate = context.Credits.FirstOrDefault(c => c.Id == creditIdToUpdate);

            if (creditToUpdate != null)
            {
                // Оновлення статусу на CLOSED, якщо поточний статус є ACTIVE
                if (creditToUpdate.CreditStatus == CreditStatus.ACTIVE)
                {
                    creditToUpdate.CreditStatus = CreditStatus.CLOSED;
                    context.SaveChanges();
                    Console.WriteLine($"Credit with Id {creditIdToUpdate} status updated to CLOSED.");
                }
                else
                {
                    Console.WriteLine($"Credit with Id {creditIdToUpdate} has a status other than ACTIVE.");
                }
            }
            else
            {
                Console.WriteLine($"Credit with Id {creditIdToUpdate} not found.");
            }
        }

        private static void DeleteClientById(BankContext? context)
        {
            int clientIdToDelete = 1; // Припустиме значення Id, яке ви хочете видалити

            var clientToDelete = context.Clients.FirstOrDefault(c => c.Id == clientIdToDelete);

            if (clientToDelete != null)
            {
                context.Clients.Remove(clientToDelete);
                context.SaveChanges();
                Console.WriteLine($"Client with Id {clientIdToDelete} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Client with Id {clientIdToDelete} not found.");
            }
        }
    }
}
