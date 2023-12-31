using EvacuationProject.BusinessLogic;
using EvacuationProject.Models;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace EvacuationProject.DataHandling
{
    public class TextDataHandler : IDataHandler
    {
        private IDataService _dataService;
        private string[] _dataFileNames;
        private List<string> _paths = new(){ "Buildings.txt", "Rooms.txt", "Workstations.txt", "Users.txt", "Administrators.txt" };

        public TextDataHandler(IDataService dataService)
        {
            _dataService = dataService;
        }
        public TextDataHandler(IDataService dataService, List<string> paths) : this(dataService)
        {
            _paths = paths;
        }
        public TextDataHandler()
        {

        }

        public void WriteDatabase()
        {
            //Building
            WriteListToTextFile(_paths[0], _dataService.Buildings);
            //Room
            WriteListToTextFile(_paths[1], _dataService.Rooms);
            //Workstation
            WriteListToTextFile(_paths[2], _dataService.Workstations);
            //User
            WriteListToTextFile(_paths[3], _dataService.Users);
            //Administrator
            WriteListToTextFile(_paths[4], _dataService.Administrators);
        }

        public void WriteListToTextFile<T>(string pathToWriteTo, List<T> listToWriteFrom) where T :class
        {
            StreamWriter myWriter = new(pathToWriteTo);
            for (int i = 0; i < listToWriteFrom.Count; i++)
            {
                myWriter.WriteLine(listToWriteFrom[i]);
            }
            myWriter.Close();
        }

        public void ReadDatabase()
        {
                //Building
                ReadItemsIntoList(_paths[0], _dataService.Buildings, ReadBuilding);
                //Room
                ReadItemsIntoList(_paths[1], _dataService.Rooms, ReadRoom);
                //Workstation
                ReadItemsIntoList(_paths[2], _dataService.Workstations, ReadWorkstation);
                //User
                ReadItemsIntoList(_paths[3], _dataService.Users, ReadUser);
                //Administrator
                ReadItemsIntoList(_paths[4], _dataService.Administrators, ReadAdministrator);
        }

        public void ReadItemsIntoList<T>(string PathToTextFile, List<T> listToReadTo, Func<string, T> myReadFunction) where T : IModel
        {
            for (int i = 0; i < listToReadTo.Count; i++)
            { 
                _dataService.Delete(listToReadTo[i], listToReadTo);
            }
            StreamReader myReader = new(PathToTextFile);
            string? input; 
            listToReadTo.Clear();
            while((input = myReader.ReadLine()) != null)
            {
                var myObj = myReadFunction(input);
                listToReadTo.Add(myObj);
                //input = myReader.ReadLine();
            }
            myReader.Close();
        }

        public Administrator ReadAdministrator(string inputString)
        {
            string[] inputArray = inputString.Split(",");
            int id = Convert.ToInt32(inputArray[0].Split(":")[1]);
            string name = inputArray[1].Split(":")[1];
            string password = inputArray[2].Split(":")[1];
            Administrator myAdmin = new(id, name, password);
            return myAdmin;
        }

        public Workstation ReadWorkstation(string arg)
        {
            string[] inputArray = arg.Split(",");
            int id = Convert.ToInt32(inputArray[0].Split(":")[1]);
            string name = inputArray[1].Split(":")[1];
            int roomId = Convert.ToInt32(inputArray[2].Split(":")[1]);
            Room myRoom = _dataService.FindObject(roomId, _dataService.Rooms);
            Workstation myWorkstation = new(name, id, myRoom);
            return myWorkstation;
        }

        public Building ReadBuilding(string input)
        {
            string[] inputArray = input.Split(",");
            int id = Convert.ToInt32(inputArray[0].Split(":")[1]);
            string name = inputArray[1].Split(":")[1];
            Building myBuilding = new(name, id);
            return myBuilding;
        }
        public Room ReadRoom(string input)
        {
            string[] inputArray = input.Split(",");
            int id = Convert.ToInt32(inputArray[0].Split(":")[1]);
            string name = inputArray[1].Split(":")[1];
            int floor = Convert.ToInt32(inputArray[2].Split(":")[1]);
            int buildingId = Convert.ToInt32(inputArray[3].Split(":")[1]);
            Building building = _dataService.FindObject(buildingId, _dataService.Buildings);
            Room myRoom = new(name, id, floor, building);
            return myRoom;
        }

        public User ReadUser(string pathToTextFile)
        {
            string[] inputArray = pathToTextFile.Split(",");
            int id = Convert.ToInt32(inputArray[0].Split(":")[1]);
            string name = inputArray[1].Split(":")[1];
            AccessLevel accessLevel = DetermineAccessLevel(inputArray[2].Split(":")[1]);
            Presence presence;
            if (inputArray[3].Split(":")[1] == "null")
                presence = null;
            else
            {
                int workstationId = Convert.ToInt32(inputArray[3].Split(":")[2]);
                Workstation myWorkstation = _dataService.FindObject(workstationId, _dataService.Workstations);
                DateTime startTime = DateTime.Parse(inputArray[4].Split(":")[1] + ":" + inputArray[4].Split(":")[2]);
                presence = new(myWorkstation, startTime);
            }
            User myUser = new(id, name, accessLevel, presence);
            return myUser;
        }

        public AccessLevel DetermineAccessLevel(string inputString)
        {
            if (inputString == "Employee")
                return AccessLevel.Employee;
            if (inputString == "Manager")
                return AccessLevel.Manager;
            throw new Exception($"Error, accesslevel not valid: {inputString}");
        }
    }
}