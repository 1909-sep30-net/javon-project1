using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.Models
{
    public class OrderLocationSearch
    {
        [DisplayName("Locations")]
        public IEnumerable<BusinessLogic.Location> Locations { get; set; }

        [DisplayName("Location ID")]
        [Required]
        public int LocationId { get; set; }
    }
}
