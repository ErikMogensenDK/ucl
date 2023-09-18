using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DraftApp.View
{
    static class MenuPresenter
    {
        public static string SelectOptionFromMenu(string menuTitle, List<string> menuItems, string menuDescription = "")
        {
            Console.Clear();
            var menu = "";
            menu += menuDescription;
            menu += "\n\n";
            var validIndexes = new Dictionary<int, string>();
            for (var i = 0; i < menuItems.Count; i++)
            {
                validIndexes[i + 1] = menuItems[i];
                menu += (i + 1) + ". " + menuItems[i] + "\n";
            }

            Console.WriteLine(menu);
            Console.Write("Choose option: ");
            var userInput = (Console.ReadLine());
            Int32.TryParse(userInput, null, out int validInput);
            while (!validIndexes.ContainsKey(validInput))
            {
                Console.WriteLine("Invalid index - pick available index");
                userInput = (Console.ReadLine());
                Int32.TryParse(userInput, null, out validInput);
            }

            return validIndexes[validInput];
        }
    }
}