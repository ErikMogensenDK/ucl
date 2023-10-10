using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvacuationProject.DataHandling
{
    public interface IDataHandler
    {
        public void ReadDatabase();
        public void WriteDatabase();
    }
}