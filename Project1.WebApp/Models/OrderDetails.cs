using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.Models
{
    public class OrderDetails
    {
        [DisplayName("Order ID")]
        public string Id { get; set; }

        [DisplayName("Location")]
        public string Location { get; set; }

        [DisplayName("Customer")]
        public string Customer { get; set; }

        [DisplayName("Order Time")]
        public string OrderTime { get; set; }

        [DisplayName("Line Items")]
        public Dictionary<BusinessLogic.Product, int> LineItems { get; set; }

        [DisplayName("Total Sale")]
        public decimal TotalSale { get; set; }
    }
}
