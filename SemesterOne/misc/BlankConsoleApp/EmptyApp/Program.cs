namespace EmptyApp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var myObj = new MyClass(15);
        Console.WriteLine(myObj.Name);
        Console.WriteLine(myObj.Age);
        myObj.PrintNameAndAge();
        myObj.Name = "testname";
        myObj.ChangeAge(200);
        myObj.PrintNameAndAge();
    }
}
