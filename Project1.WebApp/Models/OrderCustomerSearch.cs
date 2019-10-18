using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.Models
{
    public class OrderCustomerSearch
    {
        [DisplayName("Customer ID")]
        [Required]
        public int CustomerId { get; set; }
    }
}
