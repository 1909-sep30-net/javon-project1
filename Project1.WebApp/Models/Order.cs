using System.ComponentModel;

namespace Project1.WebApp.Models
{
    public class Order
    {
        [DisplayName("Order ID")]
        public string Id { get; set; }

        [DisplayName("Location ID")]
        public string LocationId { get; set; }

        [DisplayName("Customer ID")]
        public string CustomerId { get; set; }

        [DisplayName("Order Time")]
        public string OrderTime { get; set; }

        [DisplayName("Line Items")]
        public string LineItems { get; set; }
    }
}
