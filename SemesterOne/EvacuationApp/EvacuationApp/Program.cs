namespace EvacuationApp;

class Program
{
    static readonly string employeeMenuId = "employee";
    static readonly string adminMenuId = "admin";
    static void Main(string[] args)
    {
        while (true)
        {
            string selectedOption = GetOptionFromStartMenu();

            if (selectedOption == employeeMenuId)
            {
                ExecuteEmployeeAction();

                // string tempId = GetEmployeeId();

                // string tempWorkStation = GetWorkStation();

                // LogEmployeeWorkStation(tempId, tempWorkStation);
            }
            else
            {
            }
        }

    } 

    static void ExecuteEmployeeAction()
    {
    }
    static string GetEmployeeId()
    {
        string myId = R();

        // check if Id in database, and only then return
        return myId;
    }


    static string GetOptionFromStartMenu()
    {
        while (true)
        {
            WriteStartmenu();
            string userInput = R();
            if (userInput == "1")
            {
                return employeeMenuId;
            }
            else if (userInput == "2")
            {
                return "admin";
            }
            else if (userInput == "3")
            {
                return "exit";
            }
            else
            {
                P("You did not press a valid option, press enter to try again");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
    static void WriteStartmenu()
    {
        Console.Clear();
        P("Welcome to the program");
        P("Please select from the options below by pressing one of the options listed:");
        P("1. Login as employee");
        P("2. Login as admin");
        P("3. Exit the program");
    }

    static string R()
    {
       string myString = Console.ReadLine(); 
       return myString;
    }

    static void P(string myString)
    {
        Console.WriteLine(myString);
        return;
    }
}

// lige nu er printmenu og modtag menu punkt tightly coupled, så ændringer det ene sted medfører ændringer det andet sted

