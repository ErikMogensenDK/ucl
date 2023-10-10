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
        // FillDataBaseWithExamples is only for testing purposes!!
        FillDataBaseWithExamples(dataService, handler);
        handler.ReadDatabase();

	    // Could inject instead?
        ViewHelper views = new(dataService);
        views.Greeting.Run();
        while (true)
        {
            handler.ReadDatabase();
            string userOption = views.MainMenuView.Run();
            if (userOption == "Afslut")
            {
                break;
            }
            if (userOption == "Log ind")
            {
                int.TryParse(views.LoginView.Run(), out int userId);
                if (loginService.IsValidUserId(userId))
                {
                    User myUser = dataService.FindObject(userId, dataService.Users);
                    views.CreateEmployeeMainView(myUser);
                    string option = views.EmployeeMainView.Run();
                    if (option == "Check ind")
                    {
                        int.TryParse(views.EmployeeCheckinView.Run(), out int workstationIndex);
                        if (workstationIndex > dataService.Workstations.Count)
                            break;
                        Workstation myWorkstation = dataService.Workstations[workstationIndex];
                        userService.CheckIn(myUser, myWorkstation);
                        views.CreateSuccessfullCheckinView(myUser, myWorkstation);
                        views.SuccessfulCheckin.Run();
                    }
                    else if (option == "Check ud")
                    { 
                        views.CreateCheckoutView(myUser);
                        views.CheckoutView.Run();
                    }
                }
                else if (loginService.IsValidAdministrator(userId) && loginService.IsValidAdminPassword(userId, views.AdminLoginView.Run()))
                {
                    Administrator myAdmin = dataService.FindObject(userId, dataService.Administrators);
                    views.CreateAdminMainView(myAdmin);
                    string adminOption = views.AdminMainView.Run();
                    switch(adminOption)
                    {
                        case "Opret bruger":
                        {
                            views.AdminCreateUser.Run();
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Login-information var ikke gyldig, tast enter for at forsøge igen!");
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
            Console.WriteLine("Reached end of loop");
            Console.ReadLine();
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

            Administrator myAdmin = new(99999, "Test Person", "LongPassword");
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
