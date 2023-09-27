using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaticApp.Models
{
    public class CheckIn
    {
        public readonly int UserId;
        public readonly int WorkstationId;
        public readonly DateTime? StartTime = null;
        private DateTime _endtime;
        public CheckIn(int userId, int workstationId, DateTime? startTime = null)
        {
            UserId = userId;
            WorkstationId = workstationId;

            if (startTime == null)
                startTime = DateTime.Now;
            else
                StartTime = startTime;
        }
    }
}