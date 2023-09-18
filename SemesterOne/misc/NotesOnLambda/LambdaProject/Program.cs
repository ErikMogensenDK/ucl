using System.Text.Json;

namespace LambdaProject;

class Program
{
    static void Main(string[] args)
    {
        int [] myIntArray = new int [3]
        {1,2,1};

        var someVar = myIntArray.GroupBy(x => x);
        var json = JsonSerializer.Serialize(someVar);
        Console.WriteLine(json);
        var newVar = someVar.Select(g => g.Count() /2);
        json = JsonSerializer.Serialize(newVar);
        Console.WriteLine(json);

        Func<int, int> myInLine = (int x) => x*10;
        Console.WriteLine(myInLine(10));

        var Inline = (string myInput) => 5;
    }

    public static int GetLength(string s)
    {
        return s.Length;
    }

    public static void WriteAnyObject<T>(T o)
    {
        Console.WriteLine(o.ToString());
    }
}
