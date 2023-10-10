using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using EvacuationProject.BusinessLogic;
using EvacuationProject.Models;
using EvacuationProject.DataHandling;
using EvacuationProject.UI;
using System.Data;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace EvacuationProject;

class Program
{
    static void Main(string[] args)
    {
        using IHost host = CreateDefaultBuilder(args).Build();
        using var scope = host.Services.CreateScope();
        var service = scope.ServiceProvider;

        var dataService = service.GetRequiredService<IDataService>();
        var handler = service.GetRequiredService<IDataHandler>();
        var loginService = service.GetRequiredService<ILoginService>();
        var userService = service.GetRequiredService<IUserService>();
        FillDataBaseWithExamples(dataService, handler);
        handler.ReadDatabase();

	    // Could inject instead?
        ViewHelper views = new(dataService);
        views.Greeting.Run();
        while (true)
        {
            string userOption = views.MainMenuView.Run().Value.ToString();;
            if (userOption == "Log ind")
            {
                int.TryParse(views.LoginView.Run(), out int userId);
                if (loginService.IsValidUserId(userId))
                {
                    string option = views.EmployeeView.Run().Value.ToString();
                    if (option == "Check ind")
                    {
                        int.TryParse(views.EmployeeCheckinView.Run().Value.ToString(), out int workstationId);
                        userService.CheckIn(dataService.FindObject(userId, dataService.Users), dataService.FindObject(workstationId, dataService.Workstations));
                    }
                    else if (option == "Check ud")
                    { }
                }
                else if (loginService.IsValidAdministrator(userId))
                {
                    if (loginService.IsValidAdminPassword(userId, views.AdminLoginView.Run().Value.ToString()))
                    {
                        ExecuteAdminFlow();
                    }
                }
                else
                {
                    Console.WriteLine("Ukendt brugerId, tast enter for at prøve igen!");
                    Console.ReadLine();
                }

            }
            //ExecuteLoginFlow();
            //if (optionChosen == 'Employee')
            //    ExecuteEmployeeFlow();
            //if (optionChosen == 'Administrator')
            //    ExecuteAdministratorFlow();
            //if (optionChosen == 'Manager')
            //    ExecuteManagerFlow()
            handler.WriteDatabase();
        }

        IHostBuilder CreateDefaultBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<IDataService, DataService>();
                services.AddSingleton<IAdministratorService, AdministratorService>();
                services.AddSingleton<IUserService, UserService>();
                services.AddSingleton<IDataHandler, TextDataHandler>();
                services.AddSingleton<ILoginService, LoginService>();
                //services.AddSingleton<ILogger, Logger>();
            });
        }
        static void FillDataBaseWithExamples(IDataService myDataService, IDataHandler myHandler)
        {
            int userId = 123;
            string userName = "Test Name";
            AccessLevel level = AccessLevel.Employee;
            var myUser = new User(userId, userName, level);
            List<User> myUserList = new() { myUser };
            myDataService.Save(myUser, myDataService.Users);
            myDataService.Save(myUser, myDataService.Users);

            string workstationName = "Test Workstation Name";
            string roomName = "TestRum 1";
            int roomNumber = 1;
            int floor = 0;
            var myBuilding = new Building("TestBuildingName", 1);
            myDataService.Save(myBuilding, myDataService.Buildings);
            var myRoom = new Room(roomName, roomNumber, floor, myBuilding);
            myDataService.Save(myRoom, myDataService.Rooms);
            Workstation myWorkstation = new(workstationName, roomNumber, myRoom);
            myDataService.Save(myWorkstation, myDataService.Workstations);

            Administrator myAdmin = new(123, "Test Person", "LongPassword");
            myDataService.Save(myAdmin, myDataService.Administrators);
            myHandler.WriteDatabase();
            Console.WriteLine("Database successfully written!");
        }
    }



    //private static void ExecuteLoginFlow()
    //{
    //    views.Greeting.Show();
    //    optionChosen = inputHelper.ChooseOption(Views.Greeting);
    //}
    static void ExecuteAdminFlow()
    {
        throw new NotImplementedException();
    }
}
