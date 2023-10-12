using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using ConsoleTables;
using EvacuationProject.BusinessLogic;
using EvacuationProject.Models;
using Microsoft.Extensions.Options;

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
        public View SelectOptionView
        {
            get
            {
                Dictionary<int, string> validOptions = new() { { 1, "Opdater" }, { 2, "Slet" } };
                string myPrompt = "Vælg en af de viste muligheder ved at indtaste dens indeks, efterfulgt af enter";
                var myView = new View(prompt: myPrompt, validInputOptions: validOptions);
                return myView;
            }
        }
        public View MainMenuView
        {
            get
            {

                string myTitle = "Velkommen! ";
                Dictionary<int, string> validOptions = new() { { 1, "Log ind" }, { 2, "Print evakueringsliste" } };
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
                myPrompt += "\nIndtast dit administrator-kodeord efterfulgt af \"enter\"-tasten for at fortsætte";
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

        public View SuccessfulCheckin { get; private set; }
        public View CheckoutView { get; private set; }

        public View DisplayList { get; private set; }
        public View AdminCreateView { get; private set; }
        public View InvalidInput
        {
            get
            {
                string myPrompt = "Ugyldigt input, \nTryk på tasten \"enter\" for at fortsætte";
                var myView = new View(prompt: myPrompt);
                return myView;
            }
        }

        public View UpdateView { get; internal set; }
        public View EvacuationView
        {
            get
            {
                string myTitle = "Evakueringsliste";
                string myBody = DisplayEvacuationList();
                string myPrompt = "Tryk Enter for at afslutte";
                var myView = new View(title: myTitle, body: myBody, prompt: myPrompt);
                return myView;
            }
        }

        public View DeleteSuccessfull 
        {
            get{
                string myBody = "Det valgte objekt blev slettet";
                string myPrompt = "Tryk på enter-knappen for at afslutte";
                View myView = new(body: myBody, prompt: myPrompt);
                return myView;
            }
        }

        public View ChooseCrudOperation
        {
            get
            {
                string myTitle = "Administrator-menu";
                string myPrompt = "Vælg en mulighed ved at indtaste dens indeks efterfulgt af enter-tasten";
                Dictionary<int, string> myOptions = new(){{1, "Vis elementer i databasen"},{2, "Opret nyt element"}, {3, "Rediger element"}, {4, "Slet element"}};
                View myView = new(title: myTitle, validInputOptions: myOptions, prompt: myPrompt);
                return myView;
            }
        }

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

        private Dictionary<int, string> CreateOptionsDict<T>(List<T> data) where T : IModel
        {
            Dictionary<int, string> myDict = new();
            for (int i = 0; i < data.Count; i++)
            {
                myDict.Add(i + 1, data[i].Name);
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
            string myTitle = $"Velkommen {admin.Name}\nVælg hvilken type objekt du vil tilføje/redigere:";
            Dictionary<int, string> myOptions = new(){
                {1, "Bruger"},
                {2, "Administrator"},
                {3, "Bygning"},
                };
            string myPrompt = "Vælg en af mulighederne ovenfor, ved at taste dens indeks, efterfulgt af \"enter\"-tasten";
            if (dataService.Buildings.Count != 0)
            {
                myOptions.Add(4, "Lokale");
            }
            else
            {
                myPrompt += "\nVær opmærksom på, at du ikke kan oprette lokaler, før du har oprettet mindst 1 bygning";
            }
            if (dataService.Rooms.Count != 0)
            {
                myOptions.Add(5, "Arbejdsstation");
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
            Dictionary<int, string> myOptions = new() { { 1, "Check ind" }, { 2, "Check ud" } };
            var myView = new View(title: myTitle, validInputOptions: myOptions, prompt: myPrompt);
            return myView;
        }

        private object CreateEmployeeMainView()
        {
            throw new NotImplementedException();
        }
        public void CreateAdminCreateView<T>(List<T> data) where T : IModel
        {
            throw new NotImplementedException();
        }

        public void CreateDisplayListView<T>(List<T> data) where T : IModel
        {
            string myTitle = $"Eksisterende elementer i databasen: ";
            string myPrompt = "Vælg et element ved at indtaste dens indeks og trykke på \"enter\" tasten";
            //Dictionary<int, string> myOptions = new() { { 1, "Opret nyt element" }, { 2, "Opdater eksisterende element" } };
            string myBody = CreateDisplayOfList(data);
            var myView = new View(title: myTitle, prompt: myPrompt, body: myBody);
            DisplayList = myView;
        }
        private string CreateDisplayOfList<T>(List<T> data) where T : IModel
        {
            string myString;
            var table = new ConsoleTable("Indeks","Navn", "ID");
            int i = 1;
            foreach (var element in data)
            {
                table.AddRow(i, element.Name, element.Id);
                i++;
            }
            myString = table.ToMinimalString();
            return myString;
        }
        public void CreateUpdateUserIdView(int userId)
        {
            string myTitle = "Opdater Id på medarbejder";
            User myUser = dataService.FindObject(userId, dataService.Users);
            var table = new ConsoleTable("Navn", "Id");
            table.AddRow(myUser.Name, myUser.Id);
            string myBody = table.ToMinimalString();
            string myPrompt = "Indtast medarbejdernummer: ";
            var myView = new View(title: myTitle, body: myBody, prompt: myPrompt);
            UpdateView = myView;
        }
        public void CreateUpdateUserNameView(int userId)
        {
            string myTitle = "Opdater Navn på medarbejder";
            User myUser = dataService.FindObject(userId, dataService.Users);
            var table = new ConsoleTable("Navn", "Id");
            table.AddRow(myUser.Name, myUser.Id);
            string myBody = table.ToMinimalString();
            string myPrompt = "Indtast navn: ";
            var myView = new View(title: myTitle, body: myBody, prompt: myPrompt);
            UpdateView = myView;
        }
        public string DisplayEvacuationList()
        {
            List<User> myList = dataService.GetUsersCurrentlyCheckedIn();
            if (myList.Count == 0)
                return "\nIngen medarbejdere er aktuelt checket ind på arbejde \n";
            var myTable = new ConsoleTable("Navn", "Medarbejdernummer", "Sidst Tjekket ind", "Arbejdsstation",  "Lokale","Bygning");
            foreach (User myUser in myList)
            {
                myTable.AddRow(
                    myUser.Name, 
                    myUser.Id, 
                    myUser.Presence.StartTime.ToShortTimeString(), 
                    myUser.Presence.Workstation.Name, 
                    myUser.Presence.Workstation.Room.Name, 
                    myUser.Presence.Workstation.Room.Building.Name);
            }
            return myTable.ToMinimalString();
        }
        public void CreateUpdateView<T>(IDataService dataservice, T myObj) where T: class
        {
            string myTitle = "Opdater information";
            string mystring = myObj.ToString();
            string [] myStringArray = mystring.ToString().Split(",");
            List<string> prompts = new();
            foreach (string element in myStringArray)
            {
                prompts.Add($"Indtast ny værdi for {element.Split(":")[0]}, den eksisterende værdi er {element.Split(":")[1]}");
            }
            //var table = new ConsoleTable("Navn", "Id");
            //table.AddRow(myUser.Name, myUser.Id);
            //string myBody = table.ToMinimalString();
            //List<string> myPrompt = new(){"Indtast navn: ", "Indtast Id: "};
            var myView = new View(title: myTitle, prompts: prompts);
            UpdateView = myView;
        }
        public void CreateCreateView<T>(IDataService dataservice, T myObj) where T: class
        {
            string myTitle = "Opret nyt element";
            string mystring = myObj.ToString();
            string [] myStringArray = mystring.ToString().Split(",");
            List<string> prompts = new();
            foreach (string element in myStringArray)
            {
                prompts.Add($"Indtast ny værdi for {element.Split(":")[0]}, den eksisterende værdi er {element.Split(":")[1]}");
            }
            //var table = new ConsoleTable("Navn", "Id");
            //table.AddRow(myUser.Name, myUser.Id);
            //string myBody = table.ToMinimalString();
            //List<string> myPrompt = new(){"Indtast navn: ", "Indtast Id: "};
            var myView = new View(title: myTitle, prompts: prompts);
            UpdateView = myView;
        }
    }
}