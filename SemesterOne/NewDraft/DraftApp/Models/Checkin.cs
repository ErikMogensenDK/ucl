using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DraftApp.Models
{
    public class Checkin
    {
        public readonly string WorkstationId;
        public readonly string EmployeeId;
        public readonly DateTime StartTime;

        private DateTime? Endtime = null; 

        // public readonly dateTime 
        public Checkin(string employeeId, string workstationId, DateTime startTime)
        {
            EmployeeId = employeeId;
            WorkstationId = workstationId;
            StartTime = startTime;
        }

        public DateTime? GetEndTime()
        {
            if (Endtime != null)
                return Endtime;
            else
                return Endtime;
        }

        public void SetEndTime(DateTime endtime)
        {
            if (Endtime == null)
                Endtime = endtime;
        }

    }
}