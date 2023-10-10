using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvacuationProject.BusinessLogic
{
    public interface IAdministratorService
    {
        public void Create<T>(T obj, List<T> data) where T : class;
        public void Delete<T>(T obj, List<T> data) where T : class;
        public void Update<T>(T obj, List<T> data) where T : class;
        public T GetItemFromDatabase<T>(int id, List<T> data) where T : class;
    }
}