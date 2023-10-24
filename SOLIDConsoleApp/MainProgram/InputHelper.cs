using SOLIDConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.MainProgram
{
    internal class InputHelper
    {
        public static double inputDouble()
        {
            double number = 0.0;

            while (!double.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Something went wrong. Try again");
            }

            return number;
        }

        public static double inputDoubleWithHighestValue(double? equalHighestValue)
        {

            double number = 0.0;

            while (!double.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Something went wrong. Try again");
            }

            while (number > equalHighestValue)
            {
                Console.WriteLine("Not enough funds. Try input smaller amount");
                while (!double.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Something went wrong. Try again");
                }
            }

            return number;
        }

        public static double inputDoubleWithLowestValue(double? equalLowestValue)
        {

            double number = 0.0;

            while (!double.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Something went wrong. Try again");
            }

            while (number < equalLowestValue)
            {
                Console.WriteLine("Number too small. Try again");
                while (!double.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Something went wrong. Try again");
                }
            }

            return number;
        }

        public static int inputInt() 
        {
            int num = 0;

            while(!int.TryParse(Console.ReadLine(),out num))
            {
                Console.WriteLine("Something went wrong. Try again");
            }

            return num;
        }

        public static string? inputString(int? equalValue) 
        {
            string? str = Console.ReadLine();

            while (str?.Length != equalValue)
            {
                Console.WriteLine("Incorrect count of digits. Try again");
                str = Console.ReadLine();
            }

            return str;
        }

        public static void inputWait()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
