using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.Models
{
    public class OrderLocationSearch
    {
        [DisplayName("Location ID")]
        [Required]
        public int LocationId { get; set; }
    }
}
