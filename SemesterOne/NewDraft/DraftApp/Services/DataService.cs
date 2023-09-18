using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DraftApp.Models;

namespace DraftApp
{
    public class DataService
    {
        private List<Employee> _employees;
        private List<Workstation> _workstations;
        private List<Checkin> _checkins;
        public DataService(List<Employee> employees, List<Workstation> workstations, List<Checkin> checkins)
        {
            _employees = employees;
            _workstations = workstations;
            _checkins = checkins; 
        }

        public List<Checkin> GetCheckins(string employeeId)
        {
            var matchedCheckins = new List<Checkin>();
            foreach (Checkin checkin in _checkins)
            {
                if (checkin.EmployeeId == employeeId)
                    matchedCheckins.Add(checkin);
            }
            return matchedCheckins;
        }

        public List<Checkin> GetEmployeesCurrentlyCheckedIn()
        {
            List<Checkin> currentEmployeeCheckins = new();
            foreach(Checkin checkin in _checkins)
            {
                DateTime? endTime = checkin.GetEndTime();
                if (checkin.GetEndTime() == null)
                    currentEmployeeCheckins.Add(checkin);
            }
            return currentEmployeeCheckins;
        }

        internal bool EmployeeExists(string employeeId)
        {
            foreach (Employee employee in _employees)
            {
                if (employee.EmployeeId == employeeId)
                    return true;
            }
            return false;
        }

        internal void SaveCheckIn(string employeeId, string workstationId, DateTime startTime)
        {
            var newCheckin = new Checkin(employeeId, workstationId, startTime);
            _checkins.Add(newCheckin);
        }

        internal bool WorkstationExists(string workstationId)
        {
            foreach (Workstation workstation in _workstations)
            {
                if (workstation.WorkstationId == workstationId)
                    return true;
            }
            return false;
        }

        
    }
}