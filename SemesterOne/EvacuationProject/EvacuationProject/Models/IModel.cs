using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvacuationProject.Models
{
    public interface IModel
    {
        string Name {get; set; }
        int? Id {get; set;}
        public string ToString();
    }
}