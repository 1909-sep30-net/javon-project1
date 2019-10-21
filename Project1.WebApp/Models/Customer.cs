using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.Models
{
    public class Customer
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("First Name")]
        [Required]
        [StringLength(Project1.BusinessLogic.Customer.maxNameLength)]
        [RegularExpression("^[A-Za-z]+$")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        [StringLength(Project1.BusinessLogic.Customer.maxNameLength)]
        [RegularExpression("^[A-Za-z]+$")]
        public string LastName { get; set; }
    }
}
