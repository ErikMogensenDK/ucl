﻿namespace MenuApp;

class Program
{
    static void Main(string[] args)
    {
        var optionOne = "Option 1 of my fantastic menu";
        var optionTwo = "Option 2 of my fantastic menu";
        var optionThree = "Option 3 of my fantastic menu";
        var menuItems = new List<string>{optionOne, optionTwo, optionThree};
        SelectOptionFromMenu(menuItems);
        Console.WriteLine("Hello, World!");
    }

    private static void SelectOptionFromMenu(List<string> menuItems)
    {
        Console.Clear();
        var menu = "";
        menu += "Welcome to My fantastic menu! \n\n";
        var validIndexes = new Dictionary<int, string>();
        for (int i = 0; i < menuItems.Count(); i++)
        {
            menu += i+1 + ". " + menuItems[i] + "\n";
            validIndexes[i + 1] = menuItems[i];
        }
        Console.Write("Enter the number of the option you would like to choose");
        string userInput = Console.ReadLine();
        bool v = Int32.TryParse(userInput, null, out(userInput));
        while (!validIndexes.ContainsKey(userInput))
        {
            Console.WriteLine("Invalid input, try again!");
            Console.Write("Enter the number of the option you would like to choose");
        }
    }

}









































//
//        static string SelectOptionFromMenu(List<string> menuItems)
//    {
//        var menu = "";
//        var validIndexes = new Dictionary<int, string>();
//        for (var i = 0; i < menuItems.Count; i++)
//        {
//            validIndexes[i + 1] = menuItems[i];
//            menu += (i + 1) + ". " + menuItems[i] + "\n";
//        }
//
//        Console.WriteLine(menu);
//        Console.Write("Choose option: ");
//        var input = int.Parse(Console.ReadLine());
//        while (!validIndexes.ContainsKey(input))
//        {
//            Console.WriteLine("Invalid index - pick available index");
//            input = int.Parse(Console.ReadLine());
//        }
//
//        return validIndexes[input];
//    }