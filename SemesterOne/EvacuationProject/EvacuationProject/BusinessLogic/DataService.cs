using EvacuationProject.Models;
using EvacuationProject.DataHandling;
using EvacuationProject.BusinessLogic;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Reflection.Metadata.Ecma335;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;

namespace EvacuationProject.BusinessLogic
{
    public class DataService : IDataService
    {
        private List<User> _users;
        private List<Workstation> _workstations;
        private List<Administrator> _administrators;
        private List<Room> _rooms;
        private List<Building> _buildings;
        private TextDataHandler _dataHandler;


        public List<User> Users { get => _users; }
        public List<Workstation> Workstations { get => _workstations; }
        public List<Administrator> Administrators { get => _administrators; }
        public List<Room> Rooms { get =>_rooms; }
        public List<Building> Buildings { get =>_buildings; }

        public DataService()
        {
            _users = new();
            _workstations = new();
            _administrators = new();
            _rooms = new();
            _buildings = new();
            _dataHandler = new(this);
        }

        public List<User> GetUsersCurrentlyCheckedIn()
        {
            return _users.Where(u => u.Presence != null).ToList();
        }

        public bool AlreadyExists<T>(T obj, List<T> data) where T : IModel
        {
            var idProperty = typeof(T).GetProperty("Id");
            var idValue = idProperty.GetValue(obj);
            return data.Any(e => idProperty.GetValue(e).Equals(idValue));
        }

        public void Save<T>(T obj, List<T> data) where T : IModel
        {
            if (!AlreadyExists(obj, data))
                data.Add(obj);
            else
            {
                Delete(obj, data);
                data.Add(obj);
            }
        }
        public void Delete<T>(T obj, List<T> data) where T : IModel
        {
            var idProperty = typeof(T).GetProperty("Id");
            var idValue = idProperty.GetValue(obj);
            var myEnumerable = data.Where(e => idProperty.GetValue(e).Equals(idValue));
            data.Remove(myEnumerable.First());
        }
        public bool OtherObjectsDependOnThisObject<T>(T myObj) where T: IModel
        {
            if (myObj.GetType() == typeof(Room))
            {
                for (int i = 0; i < _workstations.Count; i++)
                {
                    if (_workstations[i].Room.Id == myObj.Id)
                    {
                        return true;
                    }
                }
            }
            if (myObj.GetType() == typeof(Building))
            {
                for (int i = 0; i < _rooms.Count; i++)
                {
                    if (_rooms[i].Building.Id == myObj.Id)
                        return true;
                }
            }
            return false;
        }
        public T FindObject<T>(int id, List<T> data) where T : IModel 
        {
            //try
            //{
            var idProperty = typeof(T).GetProperty("Id");
            var myEnumerable = data.Where(e => idProperty.GetValue(e).Equals(id));
            return myEnumerable.First();
            //}
            //catch
            //{
                //throw new Exception("Error - object_id not found in database");
            //}
        }
        public void DeleteObject<T>(T obj, List<T> data) where T: IModel
        {
            Delete(obj, data);
        }

    }
}