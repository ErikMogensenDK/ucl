using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIDatabase
{
    public class Employee
    {
    private readonly IDataService _dataService;
    private string userId;

    public Employee(string userId, IDataService dataService)
    {
        _dataService = dataService;
    }

    // Use the _dataService instance in your class methods
}
        public void Checkin();
        {
            _dataService.CheckinUser(this.userId);
        }
    }
