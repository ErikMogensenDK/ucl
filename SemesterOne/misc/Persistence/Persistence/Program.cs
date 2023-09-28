using System.Reflection;

namespace Persistence;

class Program
{
    static void Main(string[] args)
    {
        var myPerson = new Person("navn", DateTime.Now, 150, false, 5);
        string title = myPerson.MakeTitle();
        Console.WriteLine(title);

    }
}
