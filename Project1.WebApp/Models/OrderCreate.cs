using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Project1.WebApp.Models
{
    public class OrderCreate
    {
        [DisplayName("Location ID")]
        public int LocationId { get; set; }

        [DisplayName("Customer ID")]
        public int CustomerId { get; set; }

        [DisplayName("Inventory")]
        public Dictionary<BusinessLogic.Product, int> Inventory { get; set; }

        [DisplayName("Selected Inventory")]
        public Dictionary<int, int> SelectedInventory { get; set; }
    }
}
