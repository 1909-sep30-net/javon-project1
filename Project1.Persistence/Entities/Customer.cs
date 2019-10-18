using System;
using System.Collections.Generic;

namespace Project1.Persistence.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
