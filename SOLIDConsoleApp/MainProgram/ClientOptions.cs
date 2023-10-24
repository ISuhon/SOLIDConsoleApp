using SOLIDConsoleApp.Interfaces;
using SOLIDConsoleApp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SOLIDConsoleApp.MainProgram.Program;
using static SOLIDConsoleApp.MainProgram.InputHelper;

namespace SOLIDConsoleApp.MainProgram
{
    internal class ClientOptions
    {
        private static IClientData? client = null;
        private static ICreditCard? creditCard = null;
        public static void clientOptions()
        {
            Console.WriteLine("Hello dear client. Please enter your name:\n");
            string? name = Console.ReadLine();

            Console.WriteLine("Also enter your surname:\n");
            string? surname = Console.ReadLine();

            do
            {
                foreach (IClientData clientData in _clients)
                {
                    if (name == clientData.FirstName && surname == clientData.LastName)
                    {
                        client = clientData;
                        break;
                    }
                }

                if (client == null)
                {
                    Console.WriteLine($"I'm sorry but non found any client with name and surname \"{name} {surname}\". " +
                        "Try again or ask one of our employees to add your in our list\n" +
                        "1. Try again ?\n" +
                        "0. Return\n");

                    switch(Console.ReadKey().KeyChar)
                    {
                        case '1':
                            continue;

                        case '0':
                            return;
                    }
                }
            } while (client == null);

            while (true)
            {
                Console.WriteLine($"Hello {client?.FirstName} {client?.LastName}. What do you want to do ?\n" +
                    "1. Show credit cards info\n" +
                    "2. Choose credit card\n" +
                    "3. Show my credits\n" +
                    "0. Return\n");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        client?.Balance?.creditCards?.showCreditCards();
                        break;

                    case '2':
                        Console.Clear();
                        chooseCreditCard();
                        break;

                    case '3':
                        Console.Clear();
                        client?.Balance?.Credits?.showCredits();
                        break;
                    case '0':
                        return;
                }

                Console.Clear();
            }
        }

        public static void chooseCreditCard()
        {
            int choose = 0;
            Console.WriteLine("Which credit card you want to choose:\n");
            client?.Balance?.creditCards?.showCreditCards();

            choose = inputInt();

            while (choose < 0 || 
                choose > client?.Balance?.creditCards?.getCreditCards().Count)
            {
                Console.WriteLine("Incorrect input. Try again");
                choose = inputInt();
            }

            creditCard = client?.Balance?.creditCards?.getCreditCards()[choose];

            actionsWithCreditCard();
        }

        public static void actionsWithCreditCard()
        {
            while (true)
            {
                Console.WriteLine("What do you want to do with this card ?\n" +
                    "1. Show fortune" +
                    "2. Make a transaction" +
                    "3. Top up the fortune" +   
                    "0. Return");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine($"Your fortune : {creditCard?.Fortune}");
                        break;

                    case '2':
                        Console.Clear();
                        makeATransaction();
                        break;

                    case '3':
                        Console.Clear();
                        topUpFortune();
                        break;

                    case '0':
                        Console.Clear();
                        return;
                }
            }
        }

        public static void makeATransaction()
        {
            IClientData? recipient = new ClientData();
            ICreditCard? recipientCreditCard = new ClientCreditCard();

            Console.WriteLine("Input the card number of the person you want to make a transaction with:");
            string? recipientCardNumber = inputString(16);

            while(true)
            {
                bool endOfCycle = false;

                foreach(IClientData client in _clients)
                {
                    for(int i = 0; i < client.Balance.creditCards.getCreditCards().Count; i++)
                    {
                        if (recipientCardNumber == client.Balance.creditCards.getCreditCards()[i].CardNumber)
                        {
                            endOfCycle = true;
                            recipient = client;
                            recipientCreditCard = client.Balance.creditCards.getCreditCards()[i];
                            break;
                        }
                    }

                    if(endOfCycle)
                    {
                        break;
                    }
                }
                
                if (!endOfCycle) 
                {
                    Console.WriteLine("Looks like you make a mistake while writing card number. Try again");
                    recipientCardNumber = Console.ReadLine();
                    continue;
                }

                break;
            }

            Console.WriteLine("Enter the transaction amount");
            double amount;

            amount = inputDoubleWithHighestValue(creditCard?.Fortune);

            creditCard.Fortune -= amount;
            recipientCreditCard.Fortune += amount;

            creditCard.transactions.addTransaction(new Transaction(client?.LastName, recipient.Id, DateTime.Now, amount));
            recipientCreditCard.transactions.addTransaction(new Transaction(client?.LastName, recipient.Id, DateTime.Now, amount));
        }

        public static void topUpFortune()
        {
            int amount = 0;

            Console.WriteLine("Enter the amount of money you want to top up:");
            int.TryParse(Console.ReadLine(), out amount);

            creditCard.Fortune += amount;
        }
    }
}
