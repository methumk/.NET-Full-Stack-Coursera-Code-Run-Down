using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coreConsoleProject
{
    // employee properties but named employee
    partial class Employee
    {
        public int EmpID;
        public string EmpName;

        // Creates signature but not definition
        public partial void DisplayDetails();
    }
}
