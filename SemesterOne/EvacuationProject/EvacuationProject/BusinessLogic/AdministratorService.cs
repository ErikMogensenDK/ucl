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

        private void Save<T>(T obj, List<T> data) where T : IModel
        {
            if (_dataService.AlreadyExists<T>(obj))
                throw new Exception($"Id of \"{obj}\" already exists in in-memory-database");
            _dataService.Save(obj, data);
        }

        public void Delete<T>(T obj, List<T> data, bool overwrite = false) where T : IModel
        {
            if (!overwrite && _dataService.OtherObjectsDependOnThisObject(obj))
                throw new Exception("Fejl - kunne ikke slette, da andre objekter afhænger af objektet");
            _dataService.Delete(obj, data);
        }

        public void Update<T>(T obj, List<T> data) where T : IModel
        {
            if (_dataService.AlreadyExists<T>(obj))
                Delete(obj, data, true);
            Save(obj, data);
        }

        public T GetItemFromDatabase<T>(int id, List<T> data) where T : IModel
        {
            try
            {
                return _dataService.FindObject(id, data);
            }
            catch (Exception)
            {
                throw new Exception($"Object ID \"{id}\" not found in list {nameof(data)}");
            }
        }

        public void Create<T>(string objectString, T myObj, bool overwriteExistingObject = false) where T : IModel
        {
            if (myObj.GetType() == typeof(User))
            {
                string[] myStringArray = objectString.Split(",");
                int id;
                try {int.TryParse(myStringArray[0], out id);}
                catch{throw new Exception("id-input kunne ikke konverteres til et tal, prøv venligst igen");}
                string name = myStringArray[1];
                AccessLevel myLevel;
                switch (myStringArray[2])
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
                if (overwriteExistingObject && _dataService.AlreadyExists(oldObject))
                    Delete(oldObject, _dataService.Users, overwriteExistingObject);
                Update(newObject, _dataService.Users);
                return;
            }
            if (myObj.GetType() == typeof(Workstation))
            {
                string[] myStringArray = objectString.Split(",");
                string name = myStringArray[0];
                int roomId;
                try {int.TryParse(myStringArray[1], out roomId);}
                catch {throw new Exception("Rum-id fra arbejdsstationen kunne ikke konverteres til et tal, prøv venligst igen.");}
                Room myRoom;
                try { myRoom = GetItemFromDatabase(roomId, _dataService.Rooms); }
                catch {throw new Exception($"Fejl - kunne ikke finde rum med id: {roomId} i databasen");}

                int id;
                if (myObj.Id == null)
                    id = _dataService.Workstations.Count + 1;
                else 
                    id = myObj.Id.Value;
                Workstation oldObject = new(myObj.Name, myObj.Id, myRoom);
                Workstation newObject = new(name, id, myRoom);
                if (overwriteExistingObject && _dataService.AlreadyExists(oldObject))
                    Delete(oldObject, _dataService.Workstations, overwriteExistingObject);
                Update(newObject, _dataService.Workstations);
                return;
            }
            if (myObj.GetType() == typeof(Building))
            {
                string[] myStringArray = objectString.Split(",");
                string name = myStringArray[0];
                int? id;
                if (myObj.Id == null)
                    id = _dataService.Buildings.Count + 1;
                else 
                    id = myObj.Id;
                Building oldObject = new(myObj.Name, myObj.Id);
                Building newObject = new(name, id);
                if (overwriteExistingObject && _dataService.AlreadyExists(oldObject))
                    Delete(oldObject, _dataService.Buildings, overwriteExistingObject);
                Update(newObject, _dataService.Buildings);
                return;
            }
            if (myObj.GetType() == typeof(Administrator))
            {
                string[] myStringArray = objectString.Split(",");
                int id;
                try { int.TryParse(myStringArray[0], out id); }
                catch { throw new Exception("Kunne ikke konvertere input til id til et tal, prøv venligst igen"); }
                string name = myStringArray[1];
                string password = myStringArray[2];
                Administrator oldObject = new(myObj.Id, myObj.Name);
                Administrator newObject = new(id, name, password);
                if (overwriteExistingObject && _dataService.AlreadyExists(oldObject))
                    Delete(oldObject, _dataService.Administrators, overwriteExistingObject);
                Update(newObject, _dataService.Administrators);
                return;
            }
            if (myObj.GetType() == typeof(Room))
            {
                string[] myStringArray = objectString.Split(",");
                int id;
                if (myObj.Id == null)
                    id = _dataService.Rooms.Count + 1;
                else 
                    id = Convert.ToInt32(myObj.Id);
                string name = myStringArray[0];

                int floor;
                try{int.TryParse(myStringArray[1], out floor);}
                catch{throw new Exception("Kunne ikke konvertere etage-niveau fra rummet til et tal, prøv venligst igen");}

                int buildingId;
                try{int.TryParse(myStringArray[2], out buildingId);}
                catch{throw new Exception("Kunne ikke konvertere bygning id fra rum til et tal, prøv venligst igen");}

                Building building;
                try { building = GetItemFromDatabase(buildingId, _dataService.Buildings); }
                catch { throw new Exception($"Fejl - kunne ikke finde bygning med id-nummeret: {buildingId + 1} i databasen"); }

                Room oldObject = new(myObj.Name, myObj.Id, floor, building);
                Room newObject = new(name, id, floor, building);
                if (overwriteExistingObject && _dataService.AlreadyExists(oldObject))
                    Delete(oldObject, _dataService.Rooms, overwriteExistingObject);
                Update(newObject, _dataService.Rooms);
                return;
            }
        }
    }
}