using SOLIDConsoleApp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SOLIDConsoleApp.MainProgram.Program;
using SOLIDConsoleApp.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static SOLIDConsoleApp.MainProgram.InputHelper;
using static SOLIDConsoleApp.MainProgram.CheckHelper;

namespace SOLIDConsoleApp.MainProgram
{
    internal class EmployeeOptions
    {
        public static void employeeOptions()
        {
            while (true)
            {
                Console.WriteLine("Hello, dear employee. What do you want to do ?\n" +
                    "1. Add new client\n" +
                    "2. See list of clients\n" +
                    "3. Choose a certain client\n" +
                    "0. Return\n");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        addClient();
                        Console.Clear();
                        break;

                    case '2':
                        Console.Clear();
                        showAllClients();

                        break;

                    case '3':
                        Console.Clear();
                        chooseCertainClient();
                        break;
                    case '0':
                        Console.Clear();
                        return;

                    default:
                        Console.WriteLine("\nIncorrect symbol. Try again");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                }
            }
        }

        public static void addClient()
        {
            int ID = _clients.Count;
            string? firstName, middleName, lastName, phoneNumber, eMail;
            

            Console.WriteLine("Enter client name:\n");
            firstName = Console.ReadLine();

            Console.WriteLine("Enter client middle name:\n");
            middleName = Console.ReadLine();

            Console.WriteLine("Enter client last name:\n");
            lastName = Console.ReadLine();

            Console.WriteLine("Enter client phone number:\n");
            phoneNumber = Console.ReadLine();

            Console.WriteLine("Enter client e-mail:\n");
            eMail = Console.ReadLine();

            _clients.Add(new ClientData(ID, firstName, middleName, lastName, phoneNumber, eMail, addClientBalance(lastName)));
        }

        public static ClientBalance addClientBalance(string? surname)
        {
            ClientBalance balance = new ClientBalance();

            balance.setSurname(surname);

            balance.Credits = new Credits();

            balance.creditCards = new ClientCreditCards();

            return balance;
        }

        public static void showAllClients()
        {
            Console.WriteLine("List of all clients in our bank:\n");

            foreach (IClientData clientData in _clients)
            {
                Console.WriteLine(clientData.ToString());
                Console.WriteLine("===========================\n");
            }
        }

        public static void chooseCertainClient()
        {
            int ID = 0;
            Console.WriteLine("Please choose one of client in this list:\n");

            foreach (IClientData clientData in _clients)
            {
                Console.WriteLine(clientData.ToString());

                Console.WriteLine("===========================\n");

            }

            Console.WriteLine("Please enter ID of client you are interested in:");
            ID = inputInt();

            do
            {
                bool endOfCycle = false;
                foreach (IClientData clientData in _clients)
                {
                    if (clientData.Id == ID)
                    {
                        endOfCycle = true;
                        break;
                    }
                }

                if (!endOfCycle)
                {
                    Console.WriteLine("Maybe you make a mistake somewhere. Try again\n");
                    ID = Console.Read();
                    continue;
                }

            } while (ID > _clients.Count || ID < 0);

            while (true)
            {

                Console.WriteLine("What do you want to do with this client?\n" +
                    "1. Add credit\n" +
                    "2. Change credit status\n" +
                    "3. Show all credits\n" +
                    "4. Add credit card\n" +
                    "0. Return\n");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        addCredit(ID);
                        Console.Clear();
                        break;

                    case '2':
                        Console.Clear();
                        _clients[ID].Balance.Credits.getCreditByID(ID).CreditStatus = changeCreditStatus();
                        Console.Clear();
                        break;

                    case '3':
                        Console.Clear();
                        _clients[ID].Balance?.Credits?.showCredits();
                        break;
                    case '4':
                        Console.Clear();
                        addCreditCard(ID);
                        Console.Clear();
                        break;
                    case '0':
                        Console.Clear();
                        return;
                }
            }
        }

        public static void addCredit(int ID)
        {
            var creditType = CreditType.OPEN_CREDIT;
            DateTime date;
            double creditSum = 0.0;

            Console.WriteLine("Enter sum of credit:\n");
            creditSum = inputDoubleWithLowestValue(0);

            Console.WriteLine("What is the deadline for closing the credit:\n");

            date = dateCheck();

            Console.WriteLine("What is the type of credit:\n" +
                "1. Revolving credit" +
                "2. Installment credit" +
                "3. Open credit");

            switch(Console.ReadKey().KeyChar)
            {
                case '1':
                    creditType = CreditType.REVOLVING_CREDIT; 
                    break;

                case '2':
                    creditType = CreditType.INSTALLMENT_CREDIT; 
                    break;

                case '3':
                    creditType = CreditType.OPEN_CREDIT;
                    break;
            }

            _clients[ID]?.Balance?.Credits?.addCredit(new CreditData(creditSum, creditType, CreditStatus.ACTIVE, date));
        }

        public static CreditStatus changeCreditStatus()
        {
            
            Console.WriteLine("What status do you want to set on this credit?\n" +
            "1. Overdue\n" +
            "2. Closed\n" +
            "0. Return\n");
            
            switch(Console.ReadKey().KeyChar) 
            { 
                case '1':
                    return CreditStatus.OVERDUE;

                case '2':
                    return CreditStatus.CLOSED;

                case '0': 
                    return CreditStatus.ACTIVE;
            }

            return CreditStatus.ACTIVE;
        }

        public static void addCreditCard(int ID) 
        {
            ICreditCard creditCard = new ClientCreditCard();
            DateTime date = DateTime.Now;
            int CVV = 0, PIN = 0;

            Console.WriteLine("Enter number of credit card:\n");
            creditCard.CardNumber = inputString(16);

            Console.WriteLine("Enter expiration date of credit card:\n");
            date = dateCheck();

            Console.WriteLine("Enter CVV code of credit card:\n");
            CVV = Convert.ToInt32(inputString(3));

            Console.WriteLine("Enter a PIN");
            PIN = Convert.ToInt32(inputString(4));

            creditCard.Fortune = 0;

            _clients[ID]?.Balance?.creditCards?.addCreditCard(new ClientCreditCard(creditCard.CardNumber, date, CVV, 
                PIN, creditCard.Fortune, new TransactionHistory()));
        }
    }
}
