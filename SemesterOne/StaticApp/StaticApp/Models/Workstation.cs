using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaticApp.Models
{
    public class Workstation
    {
        public readonly int WorkstationId;
        public readonly string WorkstationName;
        public readonly Room myRoom;
        public Workstation(int workstationId, string workstationName)
        {

        }
    }
}