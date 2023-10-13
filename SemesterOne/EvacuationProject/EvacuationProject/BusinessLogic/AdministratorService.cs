using EvacuationProject.Models;
namespace EvacuationProject.BusinessLogic
{
    public class AdministratorService : IAdministratorService
    {
        private IDataService _dataService;
        public AdministratorService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Create<T>(T obj, List<T> data) where T : IModel
        {
            if (_dataService.AlreadyExists<T>(obj, data))
                throw new Exception($"Id of \"{obj}\" already exists in in-memory-database");
            _dataService.Save(obj, data);
        }
        public void Delete<T>(T obj, List<T> data) where T : IModel

        {
            if (!_dataService.AlreadyExists<T>(obj, data))
                throw new Exception($"Could not delete, since Id of \"{obj}\" does not exist in in-memory-database");
            if (_dataService.OtherObjectsDependOnThisObject(obj))
                throw new Exception("Error - could not delete, since other objects depend on this object");
            _dataService.Delete(obj, data);
        }
        public void Update<T>(T obj, List<T> data) where T : IModel
        {
            if (_dataService.AlreadyExists<T>(obj, data))
                _dataService.Delete(obj, data);
            _dataService.Save(obj, data);
        }

        public T GetItemFromDatabase<T>(int id, List<T> data) where T : IModel
        {
            try
            {
                return _dataService.FindObject(id, data);
            }
            catch (Exception)
            {
                throw new Exception($"Object ID \"{id}\" not found in database");
            }
        }

        public void CreateObject<T>(string objectString, T myObj, List<T> myList, bool owerwriteExistingObject = false) where T : IModel
        {
            if (myObj.GetType() == typeof(User))
            {
                string[] myStringArray = objectString.Split(",");
                int id;
                try {int.TryParse(myStringArray[0].Split(":")[1], out id);}
                catch{throw new Exception("id-input kunne ikke konverteres til et tal, prøv venligst igen");}
                string name = myStringArray[1].Split(":")[1];
                AccessLevel myLevel;
                switch (myStringArray[2].Split(":")[1])
                {
                    case "Employee":
                        {
                            myLevel = AccessLevel.Employee;
                            break;
                        }
                    case "Manager":
                        {
                            myLevel = AccessLevel.Manager;
                            break;
                        }
                    default:
                        {
                            myLevel = AccessLevel.Employee;
                            break;
                        }
                }
                User oldObject = new(myObj.Id, myObj.Name);
                User newObject = new(id, name, myLevel);
                if (owerwriteExistingObject && _dataService.AlreadyExists(oldObject, _dataService.Users))
                    Delete(oldObject, _dataService.Users);
                Update(newObject, _dataService.Users);
                return;
            }
            if (myObj.GetType() == typeof(Workstation))
            {
                string[] myStringArray = objectString.Split(",");
                int id;
                try {int.TryParse(myStringArray[0].Split(":")[1], out id);}
                catch { throw new Exception("id-input kunne ikke konverteres til et tal, prøv venligst igen"); }
                string name = myStringArray[1].Split(":")[1];
                int roomId;
                try {int.TryParse(myStringArray[2].Split(":")[1], out roomId);}
                catch {throw new Exception("Rum-id fra arbejdsstationen kunne ikke konverteres til et tal, prøv venligst igen.");}
                if (myObj.Id == null)
                    id = _dataService.Workstations.Count + 1;
                else 
                    id = myObj.Id.Value;
                Room myRoom = GetItemFromDatabase(roomId, _dataService.Rooms);

                Workstation oldObject = new(myObj.Name, myObj.Id, myRoom);
                Workstation newObject = new(name, id, myRoom);
                if (owerwriteExistingObject && _dataService.AlreadyExists(oldObject, _dataService.Workstations))
                    Delete(oldObject, _dataService.Workstations);
                Update(newObject, _dataService.Workstations);
                return;
            }
            if (myObj.GetType() == typeof(Building))
            {
                string[] myStringArray = objectString.Split(",");
                string name = myStringArray[1].Split(":")[1];
                int? id;
                if (myObj.Id == null)
                    id = _dataService.Buildings.Count + 1;
                else 
                    id = myObj.Id;
                Building oldObject = new(myObj.Name, myObj.Id);
                Building newObject = new(name, id);
                if (owerwriteExistingObject && _dataService.AlreadyExists(oldObject, _dataService.Buildings))
                    Delete(oldObject, _dataService.Buildings);
                Update(newObject, _dataService.Buildings);
                return;
            }
            if (myObj.GetType() == typeof(Administrator))
            {
                string[] myStringArray = objectString.Split(",");
                int id;
                try { int.TryParse(myStringArray[0].Split(":")[1], out id); }
                catch { throw new Exception("Kunne ikke konvertere input til id til et tal, prøv venligst igen"); }
                string name = myStringArray[1].Split(":")[1];
                string password = myStringArray[2].Split(":")[1];
                Administrator oldObject = new(myObj.Id, myObj.Name);
                Administrator newObject = new(id, name, password);
                if (owerwriteExistingObject && _dataService.AlreadyExists(oldObject, _dataService.Administrators))
                    Delete(oldObject, _dataService.Administrators);
                Update(newObject, _dataService.Administrators);
                return;
            }
            if (myObj.GetType() == typeof(Room))
            {
                string[] myStringArray = objectString.Split(",");
                int id;
                try{int.TryParse(myStringArray[0].Split(":")[1], out id);}
                catch{throw new Exception("Kunne ikke konvertere input til id til et tal, prøv venligst igen");}
                string name = myStringArray[1].Split(":")[1];

                int floor;
                try{int.TryParse(myStringArray[2].Split(":")[1], out floor);}
                catch{throw new Exception("Kunne ikke konvertere etage-niveau fra rummet til et tal, prøv venligst igen");}

                int buildingId;
                try{int.TryParse(myStringArray[3].Split(":")[1], out buildingId);}
                catch{throw new Exception("Kunne ikke konvertere bygning id fra rum til et tal, prøv venligst igen");}

                Building building = GetItemFromDatabase(buildingId, _dataService.Buildings);

                if (id == 0)
                    id = _dataService.Buildings.Count + 1;
                else 
                    id = myObj.Id.Value;
                Room oldObject = new(myObj.Name, myObj.Id, floor, building);
                Room newObject = new(name, id, floor, building);
                if (owerwriteExistingObject && _dataService.AlreadyExists(oldObject, _dataService.Rooms))
                    Delete(oldObject, _dataService.Rooms);
                Update(newObject, _dataService.Rooms);
                return;
            }
        }
    }
}