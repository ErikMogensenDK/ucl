using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DraftApp.Models;

namespace DraftApp
{
    public class EmployeeService
    {
        private readonly DataService _dataService;

        public EmployeeService(DataService dataService)
        {
            _dataService = dataService;
        }

        public void CheckinAtWorkstation(string employeeId, string workstationId, DateTime startTime)
        {
            Checkout(employeeId);
            if (!_dataService.EmployeeExists(employeeId))
                throw new Exception("Employee does not exist");
            if (!_dataService.WorkstationExists(workstationId))
                throw new Exception("Workstation does not exist");
            _dataService.SaveCheckIn(employeeId, workstationId, startTime);
        }
        public void Checkout(string employeeid)
        {
            List<Checkin> checkins = _dataService.GetCheckins(employeeid);
            foreach (Checkin checkin in checkins)
            {
                if (checkin.GetEndTime() == null)
                    checkin.SetEndTime(DateTime.Now);
            }
        }
    }
}