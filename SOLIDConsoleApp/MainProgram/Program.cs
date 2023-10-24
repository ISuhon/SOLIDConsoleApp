using SOLIDConsoleApp.Client;
using SOLIDConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SOLIDConsoleApp.MainProgram.EmployeeOptions;
using static SOLIDConsoleApp.MainProgram.ClientOptions;

namespace SOLIDConsoleApp.MainProgram
{
    internal class Program
    {
        internal static List<IClientData> _clients = new List<IClientData>();

        public static void Main(string[] args)
        {
            while (true)
            {

                Console.WriteLine("Hello, User, choose who you are: \n" +
                    "1. I'm a employee\n" +
                    "2. I'm a client\n" +
                    "0. Exit the program");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        employeeOptions();
                        break;

                    case '2':
                        Console.Clear();
                        clientOptions();
                        break;
                    case '0':
                        return;

                    default:
                        Console.WriteLine("\nIncorrect symbol. Try again");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                }


            }
        }

        
    }
   
}
