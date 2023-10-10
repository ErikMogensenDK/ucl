using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using EvacuationProject.BusinessLogic;
using EvacuationProject.Models;

namespace EvacuationProject.UI
{
    public class ViewHelper
    {
        public View Greeting
        {
            get
            {
                string myTitle = "Velkommen til Budwegs Evakuerings-App";
                string myPrompt = "Tryk på tasten \"enter\" for at fortsætte";

                var myView = new View(title: myTitle, prompt: myPrompt);
                return myView;
            }
        }
        public View MainMenuView
        {
            get
            {

                string myTitle = "Velkommen! ";
                Dictionary<int, string> validOptions = new() { { 1, "Log ind" }, { 2, "Print evakueringsListe" } };
                string myPrompt = "Vælg en af de viste muligheder ved at indtaste dens indeks, efterfulgt af enter";
                var myView = new View(title: myTitle, prompt: myPrompt, validInputOptions: validOptions);
                return myView;

            }
        }
        public View LoginView
        {
            get
            {
                string myPrompt = "Indtast dit 4-cifrede medarbejderID efterfulgt af \"enter\"-tasten for at fortsætte";
                var myView = new View(prompt: myPrompt);
                return myView;
            }
        }
        public View AdminLoginView
        {
            get
            {
                string myPrompt = "Indtast dit 4-cifrede medarbejderID efterfulgt af \"enter\"-tasten for at fortsætte";
                myPrompt += "\nIndtast dit administrator-kodeord efterfulgt af \"enter\"-tastenfor at fortsætte";
                var myView = new View(prompt: myPrompt);
                return myView;
            }
        }
        public View EmployeeMainView { get; private set; }
        public View AdminMainView { get; private set; }
        public View AdminBuildingEditingView { get; }
        public View AdminRoomEditingView { get; }
        public View AdminUserEditingView { get; }
        public View AdminWorkstationEditingView { get; }
        public View InvalidUserId { get; }
        public View EmployeeCheckinView
        {
            get
            {
                string myTitle = "Check ind på arbejdsstation";
                string myPrompt = "Vælg en af valgmulighederne ovenfor ved at taste dens indeks, efterfulgt af \"enter\"-tasten.";
                Dictionary<int, string> myOptions = CreateOptionsDict(dataService.Workstations);
                var myView = new View(title: myTitle, validInputOptions: myOptions, prompt: myPrompt);
                return myView;
            }
        }

        public View SuccessfulCheckin{ get; private set;}
        public View CheckoutView { get; private set; }

        public object AdminCreateView { get; private set; }

        private IDataService dataService;
        public ViewHelper(IDataService someDataService)
        {
            dataService = someDataService;
        }
        public void CreateSuccessfullCheckinView(User user, Workstation workstation)
        {

                string myPrompt = $"Checkede {user.Name} ind på arbejdsstation {workstation.Name} klokken {DateTime.Now.ToShortTimeString()}";
                myPrompt += "\nTryk på tasten \"enter\" for at fortsætte";
                var myView = new View(prompt: myPrompt);
                SuccessfulCheckin = myView;
        }
        public void CreateCheckoutView(User user)
        {
                string myPrompt = $"Checkede {user.Name} ud klokken {DateTime.Now.ToShortTimeString()}";
                myPrompt += "\nTryk på tasten \"enter\" for at fortsætte";
                var myView = new View(prompt: myPrompt);
                CheckoutView = myView;
        }
        public void CreateEmployeeMainView(User user)
        {
                string myTitle = $"Velkommen på arbejde {user.Name}!";
                string myPrompt = "Vælg en valgmulighed ved at vælge dens indeks og trykke på \"enter\" tasten";
                Dictionary<int, string> myOptions = new() { { 1, "Check ind" }, { 2, "Check ud" } };
                var myView = new View(title: myTitle, validInputOptions: myOptions, prompt: myPrompt);
                EmployeeMainView = myView;
        }

        private Dictionary <int, string> CreateOptionsDict<T>(List<T> data) where T : IModel 
        {
            Dictionary <int, string> myDict = new();
            for (int i = 0; i < data.Count; i++)
            {
                myDict.Add(i+1, data[i].Name);
            }
            return myDict;
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

        public void CreateAdminMainView(Administrator admin)
        {
            string myTitle = $"Velkommen {admin.Name}";
            Dictionary<int, string> myOptions = new(){
                {1, "Opret bruger"}, 
                {2, "Opdater eksisterende bruger"},
                {3, "Opret administrator"},
                {4, "Opdater eksisterende administrator"},
                {5, "Tilføj bygning"}, 
                {6, "Opdater eksisterende bygning"},
                };
            string myPrompt = "Vælg en af mulighederne ovenfor, ved at taste dens indeks, efterfulgt af \"enter\"-tasten";
            if (dataService.Buildings.Count != 0)
            {
                myOptions.Add(7, "Opret lokale");
                myOptions.Add(8, "Opdater eksisterende lokale");
            }
            else
            {
                myPrompt += "\nVær opmærksom på, at du ikke kan oprette lokaler, før du har oprettet mindst 1 bygning";
            }
            if (dataService.Rooms.Count != 0)
            {
                myOptions.Add(9, "Opret arbejdsstation");
                myOptions.Add(10, "Opdater eksisterende arbejdsstation");
            }
            else
            {
                myPrompt += "\nVær opmærksom på, at du ikke kan oprette arbejdsstationer, før du har oprettet mindst 1 lokale";
            }
            var myView = new View(title: myTitle, validInputOptions: myOptions, prompt: myPrompt);
            AdminMainView = myView;
        }

        private View? CreateEmployeeView()
        {
            string myTitle = "Velkommen på arbejde!";
            string myPrompt = "Vælg en valgmulighed ved at vælge dens indeks og trykke på \"enter\" tasten";
            //DictionaryList<KeyValuePair<int, string> = new();
            //KeyValuePair<int, string> myOptions = new(){{1, "Check ind"},{2, "Check ud"}};
            Dictionary<int, string> myOptions = new(){{1, "Check ind"},{2, "Check ud"}};
            var myView = new View(title: myTitle, validInputOptions: myOptions, prompt: myPrompt);
            return myView;
        }

        private object CreateEmployeeMainView()
        {
            throw new NotImplementedException();
        }
        private void CreateAdminCreateView<T>(List<T> data) where T: IModel
        {
            AdminCreateView = myView;
        }

    }
}