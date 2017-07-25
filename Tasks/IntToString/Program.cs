using System;

namespace EspTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 596;
            Program program = new Program();
            Console.WriteLine("by function: " + program.FromIntToString(i)); 
        }
        
        string FromIntToString(int number)
        {
            char[] charArray = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string result = "";
            string symbol = "";

            if (number == 0 )
            {
                return "0";
            }

            if (number < 0)
            {
                number = number * -1;
                symbol = "-";
            }
            
            while (number > 0)
            {
                var temp = number % 10;
                result = charArray[temp] + result;
                number = number / 10;
            }

            result = symbol + result;

            return result;
        }
    }
}
