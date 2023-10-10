using EvacuationProject.Models;

namespace EvacuationProject.BusinessLogic
{
    public class LoginService : ILoginService
    {
        IDataService _dataService;
        
        public LoginService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public bool IsValidAdministrator(int userId)
        {
            try
            {
                var myObj = _dataService.FindObject(userId, _dataService.Administrators);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool IsValidUserId(int userId)
        {
            try
            {
                var myObj = _dataService.FindObject(userId, _dataService.Users);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidAdminPassword(int userId, string password)
        {
            Administrator admin = _dataService.FindObject(userId, _dataService.Administrators);
            if (!(admin.Password == password))
            {
                // log admin login attempt
                //throw new Exception("Error - invalid password for Administrator");
                return false;
            }
            return true;
        }
        public User LoginUser(int userId)
        {
            User user = _dataService.FindObject(userId, _dataService.Users);
            return user;
        }
    }
}