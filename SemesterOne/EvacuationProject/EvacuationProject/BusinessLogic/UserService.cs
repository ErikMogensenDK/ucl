using EvacuationProject.Models;

namespace EvacuationProject.BusinessLogic
{
    public class UserService : IUserService
    {

        private IDataService? _dataService;

        public UserService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void CheckIn(User user, Workstation workstation)
        {
            DateTime startTime = DateTime.Now;
            Presence presence = new(workstation, startTime);
            user.Presence = presence;
            _dataService.Delete(user, _dataService.Users);
            _dataService.Save(user, _dataService.Users);
        }

        public void CheckOut(User user)
        {
            user.Presence = null;
        }
    }
}