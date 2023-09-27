using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace DisaheimDraft.Models
{
    public class Product
    {
        public readonly int GuidingSalesPrice;
        public readonly string Category;
        public readonly string Name;
        public Product(int price, string myCategory, string myName)
        {
            GuidingSalesPrice = price;
            Category = myCategory;
            Name = myName;
        }
    }
}