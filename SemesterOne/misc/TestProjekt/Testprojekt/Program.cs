using System.ComponentModel;

namespace Testprojekt;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Console.WriteLine("Write name:");
        string name = Console.ReadLine();
        Console.WriteLine("Write initials:");
        string initials = Console.ReadLine();
        Person myPerson = new(initials, name);
        Console.WriteLine($"Initialerne var: {myPerson.Initials}, navnet var {myPerson.Name}");
        Database myDatabase = new();
        Console.WriteLine($"Count before was: {myDatabase.Employees.Count()}");
        myDatabase.AddEmployee(myPerson);
        Console.WriteLine($"Count after was: {myDatabase.Employees.Count()}");
        for (int i = 0; i < myDatabase.Employees.Count; i++)
        {
            if (myDatabase.Employees[i].Name == "Erik")
            {
                Console.WriteLine("Erik was in the database");
            }
            else 
            {
                Console.WriteLine("Erik was not found in the database");
            }
        }
    }
}
