using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaheim
{
    public class Amulet
    {

        public string ItemId { get; set; }
        public string Design { get; set; }
        public Level Quality { get; set; }

        public Amulet(string itemId)
        {
            ItemId = itemId;
        }
        public Amulet(string itemId, string design) : this(itemId)
        {
            Design = design;
        }
        public Amulet(string itemId, string design, Level quality) : this(itemId, design)
        {
            Quality = quality;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}