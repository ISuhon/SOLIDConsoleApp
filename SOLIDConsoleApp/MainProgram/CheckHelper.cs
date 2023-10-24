using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static SOLIDConsoleApp.MainProgram.InputHelper;

namespace SOLIDConsoleApp.MainProgram
{
    internal class CheckHelper
    {
        public static DateTime dateCheck() 
        {
            bool endOfCycle = true;
            int year, month, day;
            DateTime date = DateTime.Now;
            do
            {
                try
                {
                    endOfCycle = false;

                    Console.WriteLine("Year:\n");
                    year = Convert.ToInt32(inputDouble());

                    Console.WriteLine("Month:\n");
                    month = Convert.ToInt32(inputDouble());

                    Console.WriteLine("Day:\n");
                    day = Convert.ToInt32(inputDouble());

                    date = new DateTime(year, month, day);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Looks like you wrote the date wrong. Try again.");
                    continue;
                }

                endOfCycle = true;
            } while (!endOfCycle);

            return date;
        }
    }
}
