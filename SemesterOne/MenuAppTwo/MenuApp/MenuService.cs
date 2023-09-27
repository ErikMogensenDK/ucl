using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuApp
{
    public class MenuService
    {
        private Menu mainMenu;
        private Menu subMenu;

        public MenuService()
        {

            subMenu = GetSubMenu();
        }

        public void ExecuteMainMenuFlow()
        {
            mainMenu = GetMainMenu();
            while (true)
            {
                mainMenu.Show();
                int choice = mainMenu.SelectMenuItem();
                if (choice == 1)
                {
                    Console.WriteLine("You pressed 1, congratulations!");
                    Console.ReadLine();
                }
                if (choice == 2)
                {
                    Console.WriteLine("You pressed 2, congratulations!");
                    Console.ReadLine();
                }
                if (choice == 3)
                {
                    subMenu.Show();
                    int subMenuchoice = 
                    subMenu.SelectMenuItem();
                }
                if (choice == 4)
                {
                    // Login as employee
                }
                if (choice == 0)
                {
                    Console.WriteLine("You pressed 0, exiting program!");
                    Console.ReadLine();
                    break;
                }
            }

        }
            public Menu GetMainMenu()
            {
                Menu mainMenu = new Menu("Min fantastiske menu");

                // First menu item
                string[] menuItems = new string[4];
                menuItems[0] = "1. Gør dit";
                menuItems[1] = "2. Gør dat";
                menuItems[2] = "3. Gå ind i undermenu!";
                menuItems[3] = "4. Få svaret på livet, universet og alting";


                foreach (string menuItem in menuItems)
                {
                    mainMenu.AddMenuItem(menuItem);
                }
                return mainMenu;
            }
            static Menu GetSubMenu()
            {
                Menu subMenu = new Menu("Min fantastiske undermenu");
                string[] subMenuItems = new string[4];
                subMenuItems[0] = "1. Gør dit (undermenuen)";
                subMenuItems[1] = "2. Gør dat (undermneuen)";
                subMenuItems[2] = "3. Gør noget";
                subMenuItems[3] = "4. Få svaret på livet, universet og alting";
                foreach (string menuItem in subMenuItems)
                {
                    subMenu.AddMenuItem(menuItem);
                }
                return subMenu;
            }
    }
}