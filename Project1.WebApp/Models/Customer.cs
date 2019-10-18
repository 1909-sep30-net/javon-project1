using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.Models
{
    public class Customer
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [Range(1, Project1.BusinessLogic.Customer.maxNameLength)]
        public string FirstName { get; set; }

        [Required]
        [Range(1, Project1.BusinessLogic.Customer.maxNameLength)]
        public string LastName { get; set; }
    }
}
