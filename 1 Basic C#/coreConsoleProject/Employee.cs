using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coreConsoleProject
{
    // Employee methods
    partial class Employee
    {
        // ctor doesn't have to be partial???
        public Employee()
        {
            this.EmpID = 0;
            this.EmpName = "Anon";
        }
        // Implements definition where other class has signature
        public partial void DisplayDetails()
        {
            Console.WriteLine($"ID {this.EmpID}\nName: {this.EmpName}");
        }
    }
}
