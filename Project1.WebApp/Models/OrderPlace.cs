using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Project1.WebApp.Models
{
    public class OrderPlace
    {
        [DisplayName("Locations")]
        public IEnumerable<SelectListItem> Locations { get; set; }

        [DisplayName("Customers")]
        public IEnumerable<SelectListItem> Customers { get; set; }
        [DisplayName("Location ID")]
        public int LocationId { get; set; }
        [DisplayName("Customer ID")]
        public int CustomerId { get; set; }
    }
}
