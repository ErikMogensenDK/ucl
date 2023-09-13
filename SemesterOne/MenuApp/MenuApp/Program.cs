
namespace MenuApp;

class Program
{
    static void Main(string[] args)
    {
        var optionOne = "Option 1 of my fantastic menu";
        var optionTwo = "Option 2 of my fantastic menu";
        var optionThree = "Option 3 of my fantastic menu";
        var menuItems = new List<string> { optionOne, optionTwo, optionThree };
        string selectedOption = SelectOptionFromMenu(menuItems);

        if (selectedOption == optionOne)
        {
            Console.WriteLine("You chose option number 1");
        }
        else if (selectedOption == optionTwo)
        {
            Console.WriteLine("You chose option number 2");
        }
        else if (selectedOption == optionThree)
        {
            Console.WriteLine("You chose option number 3");
        }
    }

    private static string SelectOptionFromMenu(List<string> menuItems)
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
        return validIndexes[intUserInput];
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