using System.Collections;

namespace MenuApp;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            displayMainMenu
            var optionOne = "Option 1 of my fantastic menu (shows sub-menu)";
            var optionTwo = "Option 2 of my fantastic menu";
            var optionThree = "Option 3 of my fantastic menu";
            var menuItems = new List<string> { optionOne, optionTwo, optionThree };
            var menuTitle = "Welcome to my fantastic menu!";
            int selectedOption = SelectOptionFromMenu(menuTitle, menuItems);
            optionOne = "You choose option number 1";
            listOfOptions = 
            ExecuteOptionFromOptions(selectedOption, listOfOptions);

            var subOptionOne = "Option 1 of my fantastic sub-menu";
            var subOptionTwo = "Option 2 of my fantastic sub-menu";
            var subOptionThree = "Option 3 of my fantastic sub-menu";
            var subMenuTitle = "Welcome to my fantastic sub-menu!";
            var subMenuItems = new List<string> {subOptionOne, subOptionTwo, subOptionThree};

            switch (selectedOption)
            {
                case 1:
                    Console.WriteLine("You chose option number 1 - sub-menu incoming");
                    int selectedSubOption = SelectOptionFromMenu(subMenuTitle, subMenuItems);
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

    static int SelectOptionFromMenu(string menuTitle, List<string> menuItems, string menuDescription = "")
    {
        Console.Clear();
        var menu = "";
        menu += menuTitle + "\n\n";
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
