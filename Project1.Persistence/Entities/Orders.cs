using System;
using System.Collections.Generic;

namespace Project1.Persistence.Entities
{
    public partial class Orders
    {
        public Orders()
        {
            LineItem = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public int LocationId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderTime { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<LineItem> LineItem { get; set; }
    }
}
