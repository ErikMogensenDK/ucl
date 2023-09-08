using System.Data;

namespace StringApp;

public class Program
{
    static void Main(string[] args)
    {
        Console.Write("Input some string, please! ");
        string input = Console.ReadLine();

        Console.Write("Input a start-position, please! ");
        int startPosition = int.Parse(Console.ReadLine());

        Console.Write("Input the length of wanted substring, please! ");
        int wantedLengthOfSubstring = int.Parse(Console.ReadLine());

        Console.Write("Input a char you want the program to search for, please! ");
        char charToSearch = char.Parse(Console.ReadLine());
        int indexOfChar = input.IndexOf(charToSearch);

        Console.WriteLine($"The string you inputted had a length of {input.Length}");
        Console.WriteLine($"The substring starting from your startposition, continuing untill the end was \"{input.Substring(startPosition)}\"");
        Console.WriteLine($"The substring resulting from your input was \"{input.Substring(startPosition, wantedLengthOfSubstring)}\"");
        if (indexOfChar == -1)
        {
            Console.WriteLine("Char was not found");
        }
        else
        {
            Console.WriteLine($"The Char was found at index {indexOfChar}");
        }
    }
}
