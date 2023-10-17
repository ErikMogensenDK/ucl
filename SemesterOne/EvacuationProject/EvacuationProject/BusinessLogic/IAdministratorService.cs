using EvacuationProject.Models;

namespace EvacuationProject.BusinessLogic
{
    public interface IAdministratorService
    {
        public void Create<T>(T obj, List<T> data) where T : IModel;
        public void Delete<T>(T obj, List<T> data, bool overwrite = false) where T : IModel;
        public void Update<T>(T obj, List<T> data) where T : IModel;
        public T GetItemFromDatabase<T>(int id, List<T> data) where T : IModel;
        public void CreateObject<T>(string objectString, T myObj, List<T> myList, bool overwriteExistingObject = false) where T : IModel;

    }
}