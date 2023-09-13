﻿using System.Collections;

namespace MenuApp;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            var optionOne = "Option 1 of my fantastic menu";
            var optionTwo = "Option 2 of my fantastic menu";
            var optionThree = "Option 3 of my fantastic menu";
            var menuItems = new List<string> { optionOne, optionTwo, optionThree };
            int selectedOption = SelectOptionFromMenu(menuItems);

            switch (selectedOption)
            {
                case 1:
                    Console.WriteLine("You chose option number 1");
                    break;
                case 2:
                    Console.WriteLine("You chose option number 2");
                    break;
                case 3:
                    Console.WriteLine("You chose option number 3");
                    break;
            }
            Console.ReadLine();
        }
    }

    static int SelectOptionFromMenu(List<string> menuItems)
    {
        Console.Clear();
        var menu = "";
        menu += "Welcome to My fantastic menu! \n\n";
        var validIndexes = new Dictionary<int, string>();
        for (int i = 0; i < menuItems.Count(); i++)
        {
            menu += i + 1 + ". " + menuItems[i] + "\n";
            validIndexes[i + 1] = menuItems[i];
        }
        Console.Write(menu);
        Console.Write("Enter the number of the option you would like to choose: ");
        string userInput = Console.ReadLine();
        bool v = Int32.TryParse(userInput, null, out int intUserInput);
        while (!validIndexes.ContainsKey(intUserInput))
        {
            Console.WriteLine("Invalid input, try again!");
            Console.Write("Enter the number of the option you would like to choose: ");
            userInput = Console.ReadLine();
            v = Int32.TryParse(userInput, null, out intUserInput);
        }
        return intUserInput;
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