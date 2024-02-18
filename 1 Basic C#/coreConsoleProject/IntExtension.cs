using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coreConsoleProject
{
    static class IntExtension
    {
        public static bool IsGreaterThan(this int i, int value) // has to be static, the this variable becomes the object to call the function from
        {
            return i > value;
        }
    }
}
