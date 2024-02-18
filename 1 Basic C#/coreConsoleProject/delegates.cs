using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Declare delegate
// Points to a function that takes one input arg
delegate int Calculate(int num);

// Anonymous delegate
// Declared like normal delegate but function is anonymous
delegate void PrintMessage(string mess);


namespace coreConsoleProject
{
    static class Delegates
    {
        static int number = 100;

        public static int addition(int num)
        {
            return (number += num);
        }

        public static int mult(int num)
        {
            return (number *= num);
        }

        // For example only for no args
        public static int GetNum()
        {
            return number;
        }

        // Assign anon method to delegate
        public static void InvokeMethod()
        {
            var printMess = "Anaon called: ";

            // Initialize the delegate
            // Calling outside variable
            PrintMessage pm = delegate(string message)
            {
                Console.WriteLine($"{printMess} {message} @ {number}");
            };

            // Invoke the delegate
            pm("Hi");
        }
    }
}
