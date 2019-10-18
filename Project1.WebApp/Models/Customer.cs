using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.Models
{
    public class Customer
    {
        [DisplayName("ID")]
        [Required]
        public string Id { get; set; }

        [DisplayName("First Name")]
        [Required]
        [Range(1, Project1.BusinessLogic.Customer.maxNameLength)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        [Range(1, Project1.BusinessLogic.Customer.maxNameLength)]
        public string LastName { get; set; }
    }
}
