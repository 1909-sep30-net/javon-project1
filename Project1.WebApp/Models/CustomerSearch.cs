using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.Models
{
    public class CustomerSearch
    {
        [DisplayName("Last Name")]
        [Required]
        [StringLength(Project1.BusinessLogic.Customer.maxNameLength)]
        [RegularExpression("^[A-Za-z]+$")]
        public string LastName { get; set; }
    }
}
