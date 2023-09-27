using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace DIDatabase
{
    public interface IDataService
    {
        void CheckinUser(string userId);        
        List<string> GetUsers();
    }
}