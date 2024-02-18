// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using coreConsoleProject;       // Namespace that contains functionality

// Arrays
/* // Multi dimensional aray
int[,,] a= new int[3,2, 1]{{{0}, {1}}, {{3}, {4}}, {{6}, {7}}};

// Jaged array
// Can access lengths from each row item
int[][] ar = new int[3][];

Console.WriteLine(a.GetLength(0));
Console.WriteLine(a.GetLength(1));
Console.WriteLine(a.GetLength(2));

for (int i=0; i < a.GetLength(0); ++i)
{
    for (int j=0; j < a.GetLength(1); ++j)
    {
        for (int k=0; k < a.GetLength(2); ++k)
        {
            Console.WriteLine($"{a[i, j, k]}");
        }
        
    }
} */



// Strings 
// Immutable strings
/* string s = "bob";
String ss = "bob";

if (s == ss)
    Console.WriteLine("same");
else
    Console.Write("Not Same");


Console.WriteLine(ss[0]);
// ss[0] = 'D'; Cannot reassign
s = "hello";    // Works but is changing memory
Console.WriteLine(ss[0]);
Console.WriteLine(s[0]);


// Mutable strng
StringBuilder sb = new StringBuilder("string Build");
sb.Append("hello");
Console.WriteLine(sb); */


// OOP
// Classes
/* Student stud = new Student();
// stud.acceptDetails();
stud.displayDetails();

// Paramaterized CTOR
Student stud2 = new Student(10, "Bob");
stud2.displayDetails();

// Copy CTOR
Student copyStud = new Student(stud);
copyStud.displayDetails(); */


// Static class
// Calls CTOR on first use
/* StaticClass.CurrCount();        // Call method directly from class
StaticClass.CurrCount();

StaticClass.count = 10;         // Call member directly from class

StaticClass.CurrCount();
StaticClass.CurrCount(); */



// Extension class
/* int i = 30;
Console.WriteLine(i.IsGreaterThan(50)); // int object extended with new functionality IsGreaterThan 
*/

// Partial class and methods
/* var employee = new Employee();
Console.WriteLine(employee.EmpID);      // Can access public property in partial class
employee.DisplayDetails();               // Can access public partial method in partial class
*/


// Class Properties
/* Student s = new Student();
s.Grade = 300;                              // Modify private variable through property (read write)

// Read only
// s.Secret = "I hate myself";              // Can't do this cuz Secrete is read only
Console.WriteLine(s.Secret);                // But can access

// Write only
s.FavoriteNumber = 99;                      // Can set
//Console.WriteLine(s.FavoriteNumber);      // Can't read write only property

// Read write with modifiers
s.Rand = 20;                                // Can set with set property
Console.WriteLine($"Rand is: {s.Rand}");    // Can get with get property
 */


// Indexer
/* Indexer idx = new Indexer();
for (int i=0; i < 10; i++)
{
    idx[i] = $"Val{i}";
}

for (int i=0; i < 10; i++)
{
    Console.WriteLine(idx[i]);
} */


// Enums
/* Indexer idx = new Indexer();
idx[(int)IndexNames.Zero] = "First element";    // Note casting enum value to int
Console.WriteLine($"idx zero: {idx[(int)IndexNames.Zero]}");
 */


//  Exception Handling
/* Indexer idx = new Indexer();
try
{
    idx[15] = "hi"; // throws exception
}
catch(Exception ex)
{
    Console.WriteLine($"{ex.Message}");
}
finally
{
    // This code alway gets executed
    Console.WriteLine("Finished");
}

// Multiple exception
try
{
    int index = int.Parse(Console.ReadLine());
    idx[index] = "hi"; // throws exception
}
catch (FormatException ex)
{
    Console.WriteLine($"FE {ex.Message}");
}
catch(Exception ex)
{
    Console.WriteLine($"{ex.Message}");
}
finally
{
    // This code alway gets executed
    Console.WriteLine("Finished");
}
 */


//  Anonymous types
/* var anonyObj = new 
{
    firstName = "Mike",
    lastName = "Hunt",
    salary = 69,
    // nested anon object
    address = new {
        streetName = "Burger ville",
        city = "MacDonnies"
    },
    // Anon object
    projects = new[]
    {
        new { projName = "Find hoes"},
        new { projName = "Divorce hoes"}
    }
};
Console.WriteLine(anonyObj);
Console.WriteLine(anonyObj.address.city);
Console.WriteLine(anonyObj.projects[0].projName);
 */


//  Delegates
// Calculate is a delegate and we create it with the new keyword and the delegate type
// The value in the parenthesis is the function to point at
// We are pointing it to the static method addition
// SINGLE CAST DELEGATE
/* Calculate da = new Calculate(Delegates.addition);   // Create a new delegate Calculate
Calculate dm = new Calculate(Delegates.mult);   // Create a new delegate Calculate

da(50);
Console.WriteLine(Delegates.GetNum());

dm(2);
Console.WriteLine(Delegates.GetNum());

// MULTIPLE CAST DELEGATE
Calculate multiple = new Calculate(Delegates.addition); // Multiple initially starts with one delegate
multiple(50);
Console.WriteLine(Delegates.GetNum());

// / This will call add + mult because we decided to add the second delegate could make it minus others as well
multiple += new Calculate(Delegates.mult);
multiple(100);                                    
Console.WriteLine(Delegates.GetNum());
 */


// Events
// Subscriber model
/* Events e = new Events();
// NOTE: delegates are static by default
// NOTE: delegate function defined after calling in delegate and after event gets raised
// Think this works cuz delegate is static?
e.eOddNum += new Events.dOddNum(EventMessage);  
e.add();
Console.ReadLine();     // Wait for operation to complete

static void EventMessage()
{
    Console.WriteLine("Odd Number Event Exectued");
}
 */


// Anonymous Methods
// Delegates.InvokeMethod();   // Calling method that invokes the delegate


// Lambda Expressions
// Expression lambda
/* var nums = new int[] {2, 4, 5, 6, 8, 5};
var count = nums.Count(x => x == 5);    // Criteria look for values that equal 5 where x is value in array
Console.WriteLine(count);

// Statement Lambda
List<int> n = new List<int>{2, 4, 5, 6, 8, 5};
count = n.Count(x => {
    if (x < 5)
    {
        return true;
    }
    return false;
});   // has a body
Console.WriteLine(count);
 */


// Expression Tree
// Assign expression lambda to function
/* Func<string, string, string> strJoin = (str1, str2) => string.Concat(str1, str2);

// Create expression
// Holds the same lambda func
// Note you cant' do: expr = strJoin;
Expression<Func<string, string, string>> expr = (str1, str2) => string.Concat(str1, str2);

// Compile the expression then use it like a regular function
var func = expr.Compile();
var result = func("Hello", "World");
Console.WriteLine(result);

//  OR

result = expr.Compile()("Hello", "Everybody");
Console.WriteLine(result); 
*/


// Async Programming
// Async programming Model (APM)