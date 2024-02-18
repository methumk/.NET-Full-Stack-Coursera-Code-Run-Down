using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;


namespace coreConsoleProject
{
    // Publisher model
    internal class Events
    {
        // Declare Delegate
        public delegate void dOddNum();


        // Declare Event
        // Note the type is of type <event delegate>
        public event dOddNum eOddNum;

        public void add()
        {
            int num1 = 100;
            int num2 = 201;
            int res = num1 + num2;
            Console.WriteLine(res);

            if (res%2 != 0 && eOddNum != null)
            {
                // Raise event
                eOddNum();
            }
        }
    }
}
