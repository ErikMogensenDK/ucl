

using System.Dynamic;

namespace AgeApp;

class Program
{
    static void Main(string[] args)
    {
        //WithoutArray();
        //WithArray();
        //WithArrayWithInitializationSyntacticSugar();
        //int ageToSearchFor = 27;
        //ArrayContains(ageToSearchFor, ageArray);
        //SmartArrayContains(ageToSearchFor, ageArray);
        //ArrayFromUserInput();
        //TryArrayFromUserInput();
        //ArrayFromUserInputUsingTryParse();

        int [] ageArray = new int [6]
        {19, 24, 22, 20, 27, 20};
        for (int i = 0; i < ageArray.Length; i+=2) 
        {
            Console.WriteLine("I was: " + i);
            Console.WriteLine("ageArray.Length was: " + ageArray.Length);
        }
    }

    private static void ArrayFromUserInputUsingTryParse()
    {
        Console.WriteLine("Getting array from user input using tryparse!");
        bool validInput = false;
        int lengthOfArray = 0;
        do
        {
            Console.Write("Input length of an array: ");
            validInput = int.TryParse(Console.ReadLine(), out lengthOfArray);
        } while (!validInput);

        int[] myArray = new int[lengthOfArray];
        for (int i = 0; i < lengthOfArray; i++)
        {
            do
            {
                Console.Write($"Input age {i + 1}:");
                validInput = int.TryParse(Console.ReadLine(), out myArray[i]);
            }
            while (!validInput);
        }
        Console.WriteLine($"The average was {myArray.Average()}");
    }

    private static void TryArrayFromUserInput()
    {
        try
        {
            ArrayFromUserInput();
        }
        catch
        {
            Console.WriteLine("OOPS, you did not input a valid exception - found by a try/catch block");
        }
    }

    private static void ArrayFromUserInput()
    {
        Console.WriteLine("Getting array from user input!");
        Console.WriteLine("Input length of an array!");
        int lengthOfArray = int.Parse(Console.ReadLine());
        int[] myArray = new int[lengthOfArray];
        for (int i = 0; i < lengthOfArray; i++)
        {
            Console.Write($"Input age {i+1}:");
            myArray[i] = int.Parse(Console.ReadLine());
        }
        Console.WriteLine($"The average was {myArray.Average()}");
    }

    private static bool SmartArrayContains(int num, int[] someArray)
    {
        Console.WriteLine("Smart implimentation of array contains:");
        if (someArray.Contains(num))
        {
            Console.WriteLine($"The array does indeed contain the number {num}");
            return true;
        }
        Console.WriteLine($"The array does not contain the number {num}");
        return false;
    }

    private static bool ArrayContains(int num, int [] someArray)
    {
        Console.WriteLine("Manual implimentation of array contains:");
        for (int i = 0; i < someArray.Length; i++)
        {
            if (someArray[i] == num)
            {
                Console.WriteLine($"The array does indeed contain the number {num}");
                return true;
            }
        }
        Console.WriteLine($"The array does not contain the number {num}");
        return false;
    }

    static void WithoutArray()
    {
        double age1 = 19;
        double age2 = 24;
        double age3 = 22;
        double age4 = 20;
        double age5 = 27;
        double age6 = 20;
        Console.WriteLine("Version implimented without using array");

        Console.WriteLine($"Age was {age1}");
        Console.WriteLine($"Age was {age2}");
        Console.WriteLine($"Age was {age3}");
        Console.WriteLine($"Age was {age4}");
        Console.WriteLine($"Age was {age5}");
        Console.WriteLine($"Age was {age6}");

        double avg = (age1 + age2 + age3 + age4 + age5 + age6)/6;
        Console.WriteLine($"Average was {avg}");
    }
    static void WithArray()
    {
        int [] ageArray = new int [6];
        ageArray[0] = 19;
        ageArray[1] = 24;
        ageArray[2] = 22;
        ageArray[3] = 20;
        ageArray[4] = 27;
        ageArray[5] = 20;
        
        Console.WriteLine("Version implimented using array");
        for(int i = 0; i < ageArray.Length; i++)
        {
            Console.WriteLine($"Age as {ageArray[i]}");
            Console.WriteLine("Age as  " + ageArray[i]);
        }
        Console.WriteLine($"Average was {ageArray.Average()}");
    }
    static void WithArrayWithInitializationSyntacticSugar()
    {
        int [] ageArray = new int [6]
        {19, 24, 22, 20, 27, 20};

        Console.WriteLine("Version implimented using array, with syntactic sugar upon intialization");

        for(int i = 0; i < ageArray.Length; i++)
        {
            Console.WriteLine($"Age as {ageArray[i]}");
        }
        Console.WriteLine($"Average was {ageArray.Average()}");
    }
}
