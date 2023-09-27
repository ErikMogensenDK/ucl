using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DraftApp.Models;

namespace DraftApp.Services
{
    public interface IDataService
    {
        List<Checkin> GetCheckins(string employeeId);
        List<Checkin> GetEmployeesCurrentlyCheckedIn();
        void SaveCheckIn(string employeeId, string workstationId, DateTime startTime);
        bool EmployeeExists(string employeeId);
        bool WorkstationExists(string workstationId);

    }
}