using EvacuationProject.Models;

namespace EvacuationProject.BusinessLogic
{
    public interface IAdministratorService
    {
        public void Delete<T>(T obj, List<T> data, bool overwrite = false) where T : IModel;
        public void Update<T>(T obj, List<T> data) where T : IModel;
        public T GetItemFromDatabase<T>(int id, List<T> data) where T : IModel;
        public void Create<T>(string objectString, T myObj, bool overwriteExistingObject = false) where T : IModel;

    }
}