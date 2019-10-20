using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.Models
{
    public class OrderCustomerSearch
    {
        [DisplayName("Customers")]
        public IEnumerable<BusinessLogic.Customer> Customers { get; set; }

        [DisplayName("Customer ID")]
        [Required]
        public int CustomerId { get; set; }
    }
}
