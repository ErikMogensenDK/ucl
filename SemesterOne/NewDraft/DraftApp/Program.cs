using DraftApp.View;
namespace DraftApp;

class Program
{
    static void Main(string[] args)
    {
        // Sets up variables needed for initial start-menu
        var employeeItemId = "employee";
        var adminItemId = "admin";
        var printEvaucationItemId = "exit";
        var exitId = "exit";
        var menuItems = new List<string> {employeeItemId, adminItemId, printEvaucationItemId};
        var menuDescription = "Welcome to the amazing Evacuation App!";

        while (true)
        {
            string option = MenuPresenter.SelectOptionFromMenu(menuDescription, menuItems);
            if (option == employeeItemId)
            {
                //ExecuteEmployeeFlow(employeeService);
                Console.WriteLine("Here's where the EmployeeMenu should've been :')");
                Console.ReadLine();
            }
            if (option == adminItemId)
            {
                //ExecuteAdminFlow(adminService);
                Console.WriteLine("Here's where the admin menu should've been :')");
                Console.ReadLine();
            }
            if (option == printEvaucationItemId)
            {
                //ExecuteAdminFlow(adminService);
                Console.WriteLine("This should print the evacuation menu!')");
                Console.ReadLine();
            }
            if (option == exitId)
            {
                Console.WriteLine("This should exit the program");
                Console.ReadLine();
            }
        }

    }
}
