using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaheim
{
    public abstract class Merchandise
    {
        public string ItemId { get; set; }

        public Merchandise() 
        {
        }
        public override string ToString()
        {
            return $"ItemId: {ItemId}";
        }
    }
}