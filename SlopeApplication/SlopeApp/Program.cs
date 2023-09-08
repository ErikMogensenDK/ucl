using System.Formats.Asn1;
namespace SlopeApp;


public class Program
{
    static void Main(string[] args)
    {
        double x1 = RequestDoubleFromConsole("Input the x1 coordinate: ");
        double y1 = RequestDoubleFromConsole("Input the y1 coordinate: ");
        double x2 = RequestDoubleFromConsole("Input the x2 coordinate: ");
        double y2 = RequestDoubleFromConsole("Input the y2 coordinate: ");

        double slope = CalculateSlope(x1, x2, y1, y2);
        Console.WriteLine($"the slope is: {slope}");
    }
    public static double RequestDoubleFromConsole(string messageToUser)
    {
        Console.Write(messageToUser);
        double userInput = double.Parse(Console.ReadLine());
        return userInput;
    }
    public static double CalculateSlope(double x1, double x2, double y1, double y2)
    {
        return (y2 - y1) / (x2 - x1);
    }
}
