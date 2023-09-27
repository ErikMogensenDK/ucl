using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DraftApp.Models;
using DraftApp.Services;

namespace DraftApp
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDataService _dataService;

        public EmployeeService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void CheckinAtWorkstation(string employeeId, string workstationId, DateTime startTime)
        {
            if (!_dataService.EmployeeExists(employeeId))
                throw new Exception("Employee does not exist");
            if (!_dataService.WorkstationExists(workstationId))
                throw new Exception("Workstation does not exist");

            // checks employee out, in case they're checked in somewhere else
            Checkout(employeeId);

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