using System.ComponentModel;

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
        public string LineItems { get; set; }
    }
}
