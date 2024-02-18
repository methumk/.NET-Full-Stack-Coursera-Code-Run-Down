using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coreConsoleProject
{
    static class StaticClass
    {
        public static int count = 0;    // can be public
        private static string name = "The static class";    // have to use static keyword here as well | if accessor not included default private
        static StaticClass()
        {
            Console.WriteLine($"StaticClass CTOR called");
            count = 0;
        }

        /* 
        // Can't have parameter ctor because they would need to call the Object itself
        static StaticClass(int C)
        {
            count = 0;
        } */

        public static void CurrCount()  // has to be static
        {
            Console.WriteLine($"Curr count is {count++}");
        }
    }
}
