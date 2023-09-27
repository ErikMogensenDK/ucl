using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MenuApp
{
    public class Menu
    {
        public string Title;
        private MenuItem[] MenuItems = new MenuItem [4];
        private int ItemCount = 0;
        private string [] options = new string [4]; 
        private int optionCount = 0;

        public Menu(string title)
        {
            Title = title;
        }
        public void Show()
        {
            Console.Clear();
            Console.WriteLine(Title + "\n");
            for (int i = 0; i < ItemCount; i++)
            {
                Console.WriteLine($"  {MenuItems[i].Title}");
            }
            Console.WriteLine("\n Indtast 0 for at afslutte, eller vælg et menupunkt");
        }

        public void AddMenuItem(string menuTitle)
        {
            MenuItem myItem = new(menuTitle);
            MenuItems[ItemCount] = myItem;
            ItemCount++;
        }

        public void AddMenuOption(string menuOption)
        {
            options[optionCount] = menuOption;
            optionCount ++;
        }

        public int SelectMenuItem()
        {
            int choice;
            string userInput;
            do
            {
                Console.Write("Select your input: ");
                userInput = Console.ReadLine();
            } while (!int.TryParse(userInput, out choice) || choice > ItemCount);
            return choice;
        }
    }
}