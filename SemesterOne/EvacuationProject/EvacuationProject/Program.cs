using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using EvacuationProject.BusinessLogic;
using EvacuationProject.Models;
using EvacuationProject.DataHandling;
using EvacuationProject.UI;

namespace EvacuationProject;

class Program
{
    static void Main(string[] args)
    {
        // Setup of Dependency injection
        using IHost host = CreateDefaultBuilder(args).Build();
        using var scope = host.Services.CreateScope();
        var service = scope.ServiceProvider;

        // Instantiating required services
        var dataService = service.GetRequiredService<IDataService>();
        var handler = service.GetRequiredService<IDataHandler>();
        var loginService = service.GetRequiredService<ILoginService>();
        var userService = service.GetRequiredService<IUserService>();
        var administratorService = service.GetRequiredService<IAdministratorService>();
        // FillDataBaseWithExamples is only for testing purposes!!
        FillDataBaseWithExamples(dataService, handler, userService);
        handler.ReadDatabase();

        // initalize views
        ViewHelper views = new(dataService);
        views.Greeting.Run();
        while (true)
        {
            string userOption = views.MainMenuView.Run();
            if (userOption == "Afslut")
            {
                break;
            }
            if (userOption == "Print evakueringsliste")
            {
                views.EvacuationView.Run();
                // Log printout of evacuation.
            }
            if (userOption == "Log ind")
            {
                int userId;
                if (int.TryParse(views.LoginView.Run(), out userId) && loginService.IsValidUserId(userId))
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
                    if (option == "Check ud")
                    {
                        views.CreateCheckoutView(myUser);
                        views.CheckoutView.Run();
                        userService.CheckOut(myUser);
                    }
                }
                else if (loginService.IsValidAdministrator(userId) && loginService.IsValidAdminPassword(userId, views.AdminLoginView.Run()))
                {
                    Administrator myAdmin = dataService.FindObject(userId, dataService.Administrators);
                    string option = "";
                    while (option != "Afslut")
                    {
                        handler.ReadDatabase();
                        views.CreateAdminMainView(myAdmin);
                        option = views.AdminMainView.Run();
                        switch (option)
                        {
                            case "Bruger":
                                {
                                    User myUser = new();
                                    ExecuteCrudFlow(dataService.Users, myUser);
                                    break;
                                }
                            case "Administrator":
                                {
                                    Administrator myAdministrator = new();
                                    ExecuteCrudFlow(dataService.Administrators, myAdministrator);
                                    break;
                                }
                            case "Bygning":
                                {
                                    Building myBuilding = new("", null);
                                    ExecuteCrudFlow(dataService.Buildings, myBuilding);
                                    break;
                                }
                            case "Lokale":
                                {
                                    Room myRoom = new("", null, 0, null);
                                    ExecuteCrudFlow(dataService.Rooms, myRoom);
                                    break;
                                }
                            case "Arbejdsstation":
                                {
                                    Workstation myWorkstation = new("", null, null);
                                    ExecuteCrudFlow(dataService.Workstations, myWorkstation);
                                    break;
                                }
                            case "Afslut":
                                {
                                    break;
                                }
                            default:
                                {
                                    views.InvalidInput.Run();
                                    break;
                                }
                        }
                        handler.WriteDatabase();
                    }
                }
                else
                {
                    Console.WriteLine("Login-information var ikke gyldig, tast enter for at forsøge igen!");
                    Console.ReadLine();
                }
            }
            handler.WriteDatabase();
        }

        IHostBuilder CreateDefaultBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<IDataService, DataService>();
                services.AddSingleton<IDataHandler, TextDataHandler>();
                services.AddTransient<IAdministratorService, AdministratorService>();
                services.AddTransient<IUserService, UserService>();
                services.AddTransient<ILoginService, LoginService>();
            });
        }
        static void FillDataBaseWithExamples(IDataService myDataService, IDataHandler myHandler, IUserService myUserService)
        {
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
            myWorkstation = new("NewWorkstationName", 2, myRoom);
            myDataService.Save(myWorkstation, myDataService.Workstations);

            Administrator myAdmin = new(0, "Test Person", "");
            myDataService.Save(myAdmin, myDataService.Administrators);

            int userId = 1234;
            string userName = "Test Thomas";
            var myUser = new User(userId, userName);
            myDataService.Save(myUser, myDataService.Users);
            myUser = new User(4321, "Tove Test");
            myDataService.Save(myUser, myDataService.Users);
            myUserService.CheckIn(myUser, myWorkstation);
            myUser = new User(4839, "Tobias Test");
            myDataService.Save(myUser, myDataService.Users);
            myUserService.CheckIn(myUser, myWorkstation);
        }

        void ExecuteCrudFlow<T>(List<T> myList, T myObj) where T : IModel
        {
            string crudOption = views.ChooseCrudOperation.Run();
            switch (crudOption)
            {
                case "Vis elementer i databasen":
                    {
                        views.CreateDisplayListView(myList);
                        views.DisplayList.Run();
                        break;
                    }
                case "Opret nyt element":
                    {
                        views.CreateCreateView(myObj);
                        string objectString = views.CreateView.Run();
                        try { administratorService.Create(objectString, myObj); }
                        catch
                        {
                            views.InvalidInput.Run();
                            break;
                        }
                        views.CreateWasASuccess.Run();
                        break;
                    }
                case "Rediger element":
                    {
                        views.CreateSelectFromListView(myList);
                        int indexOfSelectedElement;
                        while (!int.TryParse(views.SelectFromList.Run(), out indexOfSelectedElement)) ;
                        try { myObj = myList[indexOfSelectedElement - 1]; }
                        catch
                        {
                            views.InvalidInput.Run();
                            break;
                        }
                        views.CreateUpdateView(myList, myObj);
                        string objectString = views.UpdateView.Run();
                        administratorService.Create(objectString, myObj, overwriteExistingObject: true);
                        views.UpdateWasASuccess.Run();
                        break;
                    }
                case "Slet element":
                    {
                        views.CreateSelectFromListView(myList);
                        int indexOfSelectedElement;
                        while (!int.TryParse(views.SelectFromList.Run(), out indexOfSelectedElement)) ;
                        try { myObj = myList[indexOfSelectedElement - 1]; }
                        catch
                        {
                            views.InvalidInput.Run();
                            break;
                        }
                        try { administratorService.Delete(myObj, myList); }
                        catch (Exception e)
                        {
                            views.InvalidInput.Run(e.Message); break;
                        }
                        views.DeleteSuccessfull.Run();
                        break;
                    }
                case "Afslut":
                    {
                        break;
                    }
                default:
                    {
                        views.InvalidInput.Run();
                        break;
                    }
            }
        }
    }
}


