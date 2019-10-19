using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Project1.WebApp.Controllers
{
    public class LocationController : Controller
    {
        private readonly BusinessLogic.IRepository _repository;

        public LocationController(BusinessLogic.IRepository repository)
        {
            _repository = repository;
        }

        // GET: Location
        public ActionResult Index()
        {
            IEnumerable<BusinessLogic.Location> blLocations = _repository.GetAllLocations();
            return View(blLocations.Select(l => new WebApp.Models.Location
            {
                Id = l.Id.ToString(),
                Address = l.Address,
                City = l.City,
                Zipcode = l.Zipcode,
                State = l.State,
                Inventory = l.ToStringInventory()
            }));
        }
    }
}
