using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace DIDatabase
{
    public class DataService : IDataService
    {
        private List<string> _users;
        public void CheckinUser(string userId)
        {
            _users.Add(userId);
        }
        public List<string> GetUsers()
        {
            return _users;
        }
    }
}