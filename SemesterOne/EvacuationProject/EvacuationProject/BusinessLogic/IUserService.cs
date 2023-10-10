using EvacuationProject.Models;

namespace EvacuationProject.BusinessLogic
{
    public interface IUserService
    {
        public void CheckIn(User user, Workstation workstation);
        public void CheckOut(User user);
    }
}