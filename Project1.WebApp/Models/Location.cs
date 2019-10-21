using System.Collections.Generic;
using System.ComponentModel;

namespace Project1.WebApp.Models
{
    public class Location
    {
        [DisplayName("Location ID")]
        public int Id { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Zipcode")]
        public string Zipcode { get; set; }

        [DisplayName("State")]
        public string State { get; set; }

        [DisplayName("Inventory")]
        public Dictionary<BusinessLogic.Product, int> Inventory { get; set; }
    }
}
