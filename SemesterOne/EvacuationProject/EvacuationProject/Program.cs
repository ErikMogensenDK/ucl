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
        using IHost host = CreateDefaultBuilder(args).Build();
        using var scope = host.Services.CreateScope();
        var service = scope.ServiceProvider;

        var dataService = service.GetRequiredService<IDataService>();
        var handler = service.GetRequiredService<IDataHandler>();
        var loginService = service.GetRequiredService<ILoginService>();
        var userService = service.GetRequiredService<IUserService>();
        // FillDataBaseWithExamples is only for testing purposes!!
        FillDataBaseWithExamples(dataService, handler, userService);

	    // Could inject instead?
        ViewHelper views = new(dataService);
        views.Greeting.Run();
        while (true)
        {
            handler.ReadDatabase();
            Console.WriteLine(dataService.Buildings.Count);
            Console.WriteLine(dataService.Users.Count);
            foreach (User myUser in dataService.Users)
            {
                Console.WriteLine(myUser);
            }
            Console.WriteLine(dataService.Workstations.Count);
            Console.WriteLine(dataService.Administrators.Count);
            Console.ReadLine();
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
                    if (option == "Check ud")
                    { 
                        views.CreateCheckoutView(myUser);
                        views.CheckoutView.Run();
                        userService.CheckOut(myUser);
                    }
                }
                else if (loginService.IsValidAdministrator(userId) && loginService.IsValidAdminPassword(userId, views.AdminLoginView.Run()))
                {
                    string adminOption = "";
                    while (adminOption != "Afslut")
                    {
                        Administrator myAdmin = new();
                        try
                        {
                            myAdmin = dataService.FindObject(userId, dataService.Administrators);
                        }
                        catch
                        {
                            views.InvalidInput.Run();
                            break;
                        }
                        views.CreateAdminMainView(myAdmin);
                        adminOption = views.AdminMainView.Run();
                        string internalAdminOption = "";
                        switch (adminOption)
                        {
                            case "Bruger":
                                {
                                    string crudOption = views.ChooseCrudOperation.Run();

                                    //Dictionary<int, string> myOptions = new(){{1, "Vis elementer i databasen"},{2, "Opret ny element"}, {3, "Rediger element"}, {4, "Slet element"}};
                                    switch (crudOption)
                                    {
                                        case "Vis elementer i databasen":
                                        {
                                            views.CreateDisplayListView()
                                            break;
                                        }
                                        case "Opret nyt element":
                                        {
                                            break;
                                        }
                                        case "Rediger element":
                                        {
                                            break;
                                        }
                                        case "Slet element":
                                        {
                                            break;
                                        }
                                        default:
                                        {
                                            views.InvalidInput.Run();
                                            break;
                                        }
                                    }
                                    // UserCrudFlow
                                    // vælg type element (bruger, workstation osv)
                                    // vælg opret eller rediger?
                                        // opret: opretskærm
                                        // rediger: vis elementer, vælge elment
                                            // vis rediger-skærm
                                    views.CreateDisplayListView(dataService.Users);
                                    int myUserIndex;
                                    while(int.TryParse(views.DisplayList.Run(), out myUserIndex) && myUserIndex > dataService.Users.Count);
                                    User myUser = dataService.Users[myUserIndex];
                                    string selectedUserOption = views.SelectOptionView.Run();
                                    if (selectedUserOption == "Slet")
                                    {
                                        dataService.Delete(myUser, dataService.Users);
                                        views.DeleteSuccessfull.Run();
                                        break;
                                    }
                                    if (selectedUserOption == "Opdater")
                                    {
                                        views.CreateUpdateUserIdView(myUser.Id);
                                        int newUserId;
                                        do
                                        {
                                            int.TryParse(views.UpdateView.Run(), out newUserId);
                                        } while (newUserId < 0 || newUserId > 10000);
                                        views.CreateUpdateUserNameView(myUser.Id);
                                        string newName = views.UpdateView.Run();
                                        User newUser = new(newUserId, newName);
                                        dataService.Delete(myUser, dataService.Users);
                                        dataService.Save(newUser, dataService.Users);
                                        break;
                                    }
                                    break;
                                }
                            case "Administrator":
                                {
                                    views.CreateDisplayListView(dataService.Administrators);
                                    internalAdminOption = views.DisplayList.Run();
                                    break;
                                }
                            case "Bygning":
                                {
                                    views.CreateDisplayListView(dataService.Buildings);
                                    internalAdminOption = views.DisplayList.Run();
                                    break;
                                }
                            case "Lokale":
                                {
                                    views.CreateDisplayListView(dataService.Rooms);
                                    internalAdminOption = views.DisplayList.Run();
                                    break;
                                }
                            case "Arbejdsstation":
                                {
                                    views.CreateDisplayListView(dataService.Workstations);
                                    internalAdminOption = views.DisplayList.Run();
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
                else
                {
                    Console.WriteLine("Login-information var ikke gyldig, tast enter for at forsøge igen!");
                    Console.ReadLine();
                }

            }
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

            Administrator myAdmin = new(99999, "Test Person", "LongPassword");
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
            myHandler.WriteDatabase();
            Console.WriteLine("Database successfully written!");
        }
    }
//    public void CrudFlow<T>(List<T> data, IDataService dataService) 
//    {
//        views.CreateDisplayListView(data);
//        int myIndex;
//        while (int.TryParse(views.DisplayList.Run(), out myIndex) && myIndex > data.Count) ;
//        T myObj = data[myIndex];
//        string selectedUserOption = views.SelectOptionView.Run();
//        if (selectedUserOption == "Slet")
//        {
//            dataService.Delete(myObj, data);
//            break;
//        }
//        if (selectedUserOption == "Opdater")
//        {
//            // Create update view needs to know which type of object it is, since most objects have different info 
//            // For instance a room needs to be associated to a building and a workstation to a oom
//            // Can a view take prompts in succession??
//            views.CreateUpdateView(myUser.Id);
//            int newUserId;
//            do
//            {
//                int.TryParse(views.UpdateView.Run(), out newUserId);
//            } while (newUserId < 0 || newUserId > 10000);
//            views.CreateUpdateUserNameView(myUser.Id);
//            string newName = views.UpdateView.Run();
//            User newUser = new(newUserId, newName);
//            dataService.Delete(myUser, data);
//            dataService.Save(newUser, data);
//            break;
//        }
//        break;
//    }
}
