using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Linq;
using System.Threading.Tasks;

namespace DraftApp.Services
{
        public interface IEmployeeService
    {
        void CheckinAtWorkstation(string employeeId, string workstationId, DateTime StartTime);
        void Checkout(string employeeId);
    }
}