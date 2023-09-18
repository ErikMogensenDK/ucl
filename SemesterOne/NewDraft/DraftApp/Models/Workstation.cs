using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DraftApp.Models
{
    public class Workstation
    {
        public readonly string WorkstationId;

        public Workstation(string workstationId)
        {
            WorkstationId = workstationId;
        }
    }
}