using System;
using System.Collections.Generic;

namespace Project1.Persistence.Entities
{
    public partial class Product
    {
        public Product()
        {
            Inventory = new HashSet<Inventory>();
            LineItem = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<LineItem> LineItem { get; set; }
    }
}
