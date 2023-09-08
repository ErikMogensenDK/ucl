using System.Formats.Asn1;

namespace UclApp;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Input the length of a rectangle: ");
        int length = int.Parse(Console.ReadLine());
        Console.Write("Input the width of a rectangle: " );
        int width = int.Parse(Console.ReadLine());

        int area = width * length;
        Console.WriteLine($"The area of the the rectangle is: {area}");
    }
}
