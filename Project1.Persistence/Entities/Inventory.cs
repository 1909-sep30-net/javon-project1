using System;
using System.Collections.Generic;

namespace Project1.Persistence.Entities
{
    public partial class Inventory
    {
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
    }
}
