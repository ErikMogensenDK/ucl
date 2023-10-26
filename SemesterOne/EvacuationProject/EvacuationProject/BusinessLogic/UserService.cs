using EvacuationProject.Models;
using EvacuationProject.DataHandling;

namespace EvacuationProject.BusinessLogic
{
    public class UserService : CrudService
    {

        private IDataService? _dataService;
        private List<User> _users;

        public UserService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void CheckIn(User user, Workstation workstation)
        {
            DateTime startTime = DateTime.Now;
            Presence presence = new(workstation, startTime);
            user.Presence = presence;
            _dataService.Save(user, _dataService.Users);
        }

        public void CheckOut(User user)
        {
            user.Presence = null;
        }
        public void Save(User user)
        {
            if (!_dataService.AlreadyExists(user))
                _dataService.Users.Add(user);
        }
        public void Delete(User user)
        {
            if (_dataService.AlreadyExists(user))
                _dataService.Delete(user, dataService);
        }
        public User Read(User user)
        {
            return _dataService.FindObject(user.Id);
        }
        private User Read(string pathToTextFile)
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
        private AccessLevel DetermineAccessLevel(string inputString)
        {
            if (inputString == "Employee")
                return AccessLevel.Employee;
            if (inputString == "Manager")
                return AccessLevel.Manager;
            throw new Exception($"Error, accesslevel not valid: {inputString}");
        }
    }

    public class CrudService<T>
    {
        TextDataHandler _dataHandler = new();
        List<T> myList = new List<T>();
        public void Save(T myObj)
        {
        }
    }
}