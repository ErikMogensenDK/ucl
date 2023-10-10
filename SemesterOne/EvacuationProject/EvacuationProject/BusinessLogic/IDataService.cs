using EvacuationProject.Models;

namespace EvacuationProject.BusinessLogic
{
    public interface IDataService
    {
        public List<User> Users { get; }
        public List<Workstation> Workstations { get; }
        public List<Administrator> Administrators { get; }
        public List<Room> Rooms { get; }
        public List<Building> Buildings { get; }

        public List<User> GetUsersCurrentlyCheckedIn();
        public bool AlreadyExists<T>(T obj, List<T> data) where T : class;
        public void Save<T>(T obj, List<T> data) where T : class;
        public void Delete<T>(T obj, List<T> data) where T : class;
        public T FindObject<T>(int Id, List<T> data) where T : class;
    }
}