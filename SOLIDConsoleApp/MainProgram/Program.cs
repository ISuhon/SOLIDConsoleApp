using SOLIDConsoleApp.Client;
using SOLIDConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using static SOLIDConsoleApp.MainProgram.EmployeeOptions;
using static SOLIDConsoleApp.MainProgram.ClientOptions;
using SOLIDConsoleApp.DataBase;

namespace SOLIDConsoleApp.MainProgram
{
    internal class Program
    {
        internal static List<IClientData> _clients = new List<IClientData>();

       

        public static void Main(string[] args)
        {
            using(ClientContext clientDB = new ClientContext())
            {
                ClientData client1 = new ClientData(0, "Vasya", "Popovich", "Pupkin", "+380678954756", "vasyapupkin@gmail.com", 0);
                ClientData client2 = new ClientData(1, "Pedro", "Popovich", "Pupkin", "+380678954623", "pedropupkin@gmail.com", 1);

                clientDB.Clients.Add(client1);
                clientDB.SaveChanges();
                Console.WriteLine("Clients added to database successfully");

                var clients = clientDB.Clients;
                Console.WriteLine("List of clients from db : ");
                foreach(var client in clients)
                {
                    Console.WriteLine(client);
                }
            }
            //while (true)
            //{

            //    Console.WriteLine("Hello, User, choose who you are: \n" +
            //        "1. I'm a employee\n" +
            //        "2. I'm a client\n" +
            //        "0. Exit the program");

            //    switch (Console.ReadKey().KeyChar)
            //    {
            //        case '1':
            //            Console.Clear();
            //            employeeOptions();
            //            break;

            //        case '2':
            //            Console.Clear();
            //            clientOptions();
            //            break;
            //        case '0':
            //            return;

            //        default:
            //            Console.WriteLine("\nIncorrect symbol. Try again");
            //            System.Threading.Thread.Sleep(1000);
            //            Console.Clear();
            //            break;
            //    }


            //}
        }

        
    }
   
}
