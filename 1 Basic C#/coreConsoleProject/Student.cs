using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coreConsoleProject
{
    internal class Student
    {
        // Private member variables (private by default)
        int studentID;
        string studentName;

        // Public Property | Private field
        private int wtf {get; set;}     // Get set auto implemented even for private also (can't get from outside though)

        // Read-Write property
        private int grade;
        public int Grade
        {
            get {return grade;}
            set {grade = value;}
        }

        // readonly property
        private string secret;
        public string Secret
        {
            get => secret;      // Express bodied syntax
        }

        // Write only property
        private int favoriteNumber;
        public int FavoriteNumber
        {
            set => favoriteNumber = value *2;       //Express bodied
        }

        private int rand;
        public int Rand
        {
            get {return 2*rand;}
            set 
            {
                if (value < 0)
                {
                    throw new Exception("Negative Value");
                }

                var r = new Random();
                rand = r.Next(value);
                Console.WriteLine($"Random value: {rand}");
                
            }
        }

        // Public CTOR
        public Student()
        {
            studentID = 0;
            studentName = "Anon";
            secret = "I like turtled";
        }

        // Parameterized Ctor
        public Student(int ID, String name)
        {
            studentID = ID;
            studentName = name;
            grade = 0;
        }

        // Copy CTOR
        public Student(Student s)
        {
            // Note can access private variables
            studentID = s.studentID;
            studentName = s.studentName;
            grade = s.grade;
        }

        public void displayDetails()
        {
            Console.Write($"Student ID: {studentID}\n Name: {studentName}\n");
        }

        public void acceptDetails()
        {
            Console.Write("Enter ID: ");
            studentID = int.Parse(Console.ReadLine());
            Console.Write("Enter Name: ");
            studentName = Console.ReadLine();
        }
    }

    // NOTE: 
    /* static void GetName()
    {

    } */
}
