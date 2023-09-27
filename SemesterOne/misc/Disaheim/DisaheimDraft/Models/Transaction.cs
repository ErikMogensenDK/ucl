using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DisaheimDraft.Models
{
    public class Transaction
    {
        public readonly int Price;
        public readonly DateTime TimeOfTransaction;
        public readonly Product Product;
        public readonly bool IsSale;

        public Transaction(int price, DateTime timeOfSale, Product product, bool isSale)
        {
            Price = price;
            TimeOfTransaction= timeOfSale;
            Product = product;
            IsSale = isSale;
        }
    }
}