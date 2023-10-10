using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvacuationProject.BusinessLogic
{
    public interface ILoginService
    {
        public bool IsValidUserId(int userId);
        public bool IsValidAdministrator(int userId);
        public bool IsValidAdminPassword(int userId, string password);

    }
}