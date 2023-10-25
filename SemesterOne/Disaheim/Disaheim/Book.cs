using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Disaheim
{
    public class Book
    {
        public string ItemId { get; set; }
        public string? Title { get; set; }
        public double Price { get; set; }
    public Book(string itemId)
    {
        ItemId = itemId;
    }
    public Book(string itemId, string title):this(itemId)
    {
        Title = title;        
    }
    public Book(string itemId, string title, double price):this(itemId, title)
    {
        Price = price;
    }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}