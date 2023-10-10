
namespace EvacuationProject.BusinessLogic
{
    public class AdministratorService : IAdministratorService
    {
        private IDataService _dataService;
        public AdministratorService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Create<T>(T obj, List<T> data) where T : class
        {
            if (_dataService.AlreadyExists<T>(obj, data))
                throw new Exception($"Id of \"{obj}\" already exists in in-memory-database");
            _dataService.Save(obj, data);
        }
        public void Delete<T>(T obj, List<T> data) where T : class

        {
            if (!_dataService.AlreadyExists<T>(obj, data))
                throw new Exception($"Could not delete, since Id of \"{obj}\" does not exist in in-memory-database");
            _dataService.Delete(obj, data);
        }
        public void Update<T>(T obj, List<T> data) where T : class
        {
            if (!_dataService.AlreadyExists<T>(obj, data))
                throw new Exception($"Could not update, since Id of \"{obj}\" does not exist in in-memory-database");
            _dataService.Delete(obj, data);
            _dataService.Save(obj, data);
        }

        public T GetItemFromDatabase<T>(int id, List<T> data) where T : class
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
    }
}