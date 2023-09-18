using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Dynamic;
using System.Reflection.Metadata;
using Microsoft.VisualBasic;

namespace DraftApp;
class Program
{
    static void Main(string[] args)
    {
        DatabaseService databaseService = new();
        EmployeeService employeeService = new (databaseService);
        AdminService adminService = new (databaseService);

        // int numberOfTestusers = 10;
        // populateDatabaseWithTestData(databaseService, numberOfTestUsers);

        // Sets up variables needed for initial start-menu
        var employeeItemId = "employee";
        var adminItemId = "admin";
        // Currenly exitid does not do anything, because this is the "outer-most" menu
        //var exitId = "exit";
        var menuItems = new List<string> { employeeItemId, adminItemId};
        var menuDescription = "Welcome to the amazing Evacuation App!";

        while (true)
        {
            string option = SelectOptionFromMenu(menuDescription, menuItems);
            if (option == employeeItemId)
            {
                ExecuteEmployeeFlow(employeeService);
            }
            if (option == adminItemId)
            {
                ExecuteAdminFlow(adminService);
            }
        }
    }

    private static void ExecuteAdminFlow(AdminService adminService)
    {
        // Print new menu to console
        var createUserMenuId = "Create user";
        var createWorkstationMenuId = "Create workstation";
        var exitId = "Exit";
        var menuItems = new List<string> { createUserMenuId, createWorkstationMenuId, exitId };
        var menuDescription = "Welcome to the menu for registration of workstation for employees";
        var option = SelectOptionFromMenu(menuDescription, menuItems);
        if (option == registerWorkstationMenuId)
        {
            ExecuteRegisterWorksStationFlow(employeeService);
        }
    }

    static void ExecuteEmployeeFlow(EmployeeService employeeService)
    {
        // Print new menu to console
        var registerWorkstationMenuId = "Register workstation";
        var exitId = "Exit";
        var menuItems = new List<string> { registerWorkstationMenuId, exitId };
        var menuDescription = "Welcome to the menu for registration of workstation for employees";
        var option = SelectOptionFromMenu(menuDescription, menuItems);
        if (option == registerWorkstationMenuId)
        {
            ExecuteRegisterWorksStationFlow(employeeService);
        }
    }

    static void ExecuteRegisterWorksStationFlow(EmployeeService employeeService)
    {
        Console.Write("Enter userId, workstationId: ");
        var input = Console.ReadLine();
        var split = input.Split(",");
        var userId = split[0];
        var workStationId = split[1];
        var result = employeeService.TryRegisterWorkStation(userId, workStationId);
        if (!result.IsSuccess)
        {
            Console.WriteLine(result.ErrorMessage);
        }
        else
        {
            Console.WriteLine("Registration was a success.");
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadLine();
        }
    }

    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class EmployeeService
    {
        private readonly DatabaseService _dbService = new DatabaseService();
        public EmployeeService(DatabaseService _dbService)
        {
            var databaseService = _dbService;
        }

        public ResultDto TryRegisterWorkStation(string userId, string workStationId)
        {
            if (String.IsNullOrEmpty(userId))
                return new ResultDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Empty User id is not allowed"
                };
            var doesUserExist = _dbService.CheckUserExists(userId);
            if (!doesUserExist)
                return new ResultDto
                {
                    IsSuccess = false,
                    ErrorMessage = "User does not exist in database"
                };

            var workStationExists = _dbService.CheckWorkstationExists(workStationId);
            if (!workStationExists)
                return new ResultDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Workstation does not exist in database"
                };

            _dbService.SaveWorkstationRegistration(userId, workStationId);
            return new ResultDto
            {
                IsSuccess = true
            };
        }
    }


    static string SelectOptionFromMenu(string menuDescription, List<string> menuItems)
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

    public class DatabaseService
    {

        List<EmployeeDto> _employees = new List<EmployeeDto>();
        List<WorkstationDto> _workstations = new List<WorkstationDto>();
        public DatabaseService()
        {
            var _employees = new List<EmployeeDto>();
            var _workstations = new List<WorkstationDto>();
        }

        public bool DoesEmployeeExist(int employeeId)
        {
            for (int i = 0; i < _employees.Count; i++)
            {
                if (_employees[i].EmployeeId== employeeId)
                {
                    return true;
                }
            }
            // returns false if no matches
            return false;
        }

        internal bool CheckUserExists(string userId)
        {
            throw new NotImplementedException();
        }

        internal bool CheckWorkstationExists(string workStationId)
        {
            throw new NotImplementedException();
        }

        internal void SaveWorkstationRegistration(string userId, string workStationId)
        {
            throw new NotImplementedException();
        }
    }

    class EmployeeDto
    {
        public int EmployeeId { get; set; }
    }

    class WorkstationDto
    {
        public string WorkStationId { get; set; }
    }
}

class AdminService
{
    private Program.DatabaseService databaseService;

    public AdminService(Program.DatabaseService databaseService)
    {
        this.databaseService = databaseService;
    }
}