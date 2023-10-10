using EvacuationProject.BusinessLogic;
using EvacuationProject.Models;

namespace EvacuationProject.UI
{
    public class ViewHelper
    {
        public View Greeting { get; }
        public View MainMenuView { get; }
        public View LoginView { get; }
        public View AdminLoginView { get; }
        public View EmployeeView { get; }
        public View AdminMainView { get; }
        public View AdminBuildingEditingView { get; }
        public View AdminRoomEditingView { get; }
        public View AdminUserEditingView { get; }
        public View AdminWorkstationEditingView { get; }
        public View InvalidUserId { get; }
        public View EmployeeCheckinView
        {
            get
            {
            string myTitle = "Velkommen!";
            Dictionary<int, string> validOptions = new();
            var myOptions = CreateOptionList(dataService.Workstations);
            string myPrompt = "Vælg en af de viste muligheder ved at indtaste dens indeks, efterfulgt af enter";
            var myView = new View(title: myTitle, prompt: myPrompt, validInputOptions: myOptions);
            return myView;
            }
        }
        private IDataService dataService;

        public ViewHelper(IDataService someDataService)
        {
            Greeting = CreateGreetingView();
            MainMenuView = CreateMainMenuView();
            LoginView = CreateLoginView();
            //AdminLoginView = CreateAdminLoginView();
            EmployeeView= CreateEmployeeView();
        //    EmployeeCheckinView = CreateEmployeeCheckinView();
            //AdminMainView = CreateAdminMainView();
            //AdminBuildingEditingView = CreateAdminBuildingEditor();
            //AdminRoomEditingView = CreateAdminRoomEditor();
            //AdminUserEditingView = CreateAdminUserEditor();
            //AdminWorkstationEditingView = CreateAdminWorkstationEditor();
            InvalidUserId = CreateInvalidUserIdView();

            dataService = someDataService;
        }

        private List<KeyValuePair<int, string>> CreateOptionList<T>(List<T> data) where T : IModel 
        {
            List<KeyValuePair<int, string>> myList = new();
            for (int i = 0; i < data.Count; i++)
            {
                KeyValuePair<int, string> myPair = new(data[i].Id, data[i].Name);
                myList.Add(myPair);
            }
            return myList;
        }

        private View? CreateMainMenuView()
        {
            string myTitle = "Velkommen! ";
            var validOptions = new List<KeyValuePair<int, string>>() {
                new KeyValuePair<int, string>(1, "Log ind"),
                new KeyValuePair<int, string>(2, "Print evakueringsliste")
            };
            string myPrompt = "Vælg en af de viste muligheder ved at indtaste dens indeks, efterfulgt af enter";
            var myView = new View(title: myTitle, prompt: myPrompt, validInputOptions: validOptions);
            return myView;

        }

        private View? CreateInvalidUserIdView()
        {
            string myTitle = "BrugerId blev ikke fundet i databasen";
            string myPrompt = "Tryk enter for at fortsætte";
            var myView = new View(title: myTitle, prompt: myPrompt);
            return myView;
        }

        private object CreateAdminLoginView()
        {
            throw new NotImplementedException();
        }

        private View CreateLoginView()
        {
            string myPrompt = "Indtast dit 4-cifrede medarbejderID for at fortsætte";
            var myView = new View(prompt: myPrompt);
            return myView;
        }

        private object? CreateAdminWorkstationEditor()
        {
            throw new NotImplementedException();
        }

        private object? CreateAdminUserEditor()
        {
            throw new NotImplementedException();
        }

        private object? CreateAdminRoomEditor()
        {
            throw new NotImplementedException();
        }

        private object? CreateAdminBuildingEditor()
        {
            throw new NotImplementedException();
        }

        private object? CreateAdminMainView()
        {
            throw new NotImplementedException();
        }

        private View? CreateEmployeeView()
        {
            string myTitle = "Velkommen på arbejde!";
            string myPrompt = "Vælg en valgmulighed ved at vælge dens indeks og trykke på \"enter\" tasten";
            List<KeyValuePair<int, string>> myList = new(){
                new KeyValuePair<int, string> (1, "Check ind"),
                new KeyValuePair<int, string> (2, "Check ud")
            };
            var myView = new View(title: myTitle, validInputOptions: myList, prompt: myPrompt);
            return myView;
        }

        private object CreateEmployeeMainView()
        {
            throw new NotImplementedException();
        }

        private View? CreateGreetingView()
        {
            string myTitle = "Velkommen til Budwegs Evakuerings-App";
            string myPrompt = "Tryk på tasten \"enter\" for at fortsætte";

            var myView = new View(title: myTitle, prompt: myPrompt);
            return myView;
        }
    }
}