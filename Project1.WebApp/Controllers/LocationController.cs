using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
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

        /// <summary>
        /// GET: Location
        /// </summary>
        /// <returns>The list of locations</returns>
        public ActionResult Index()
        {
            return View(_repository.GetAllLocations().Select(l => new WebApp.Models.Location
            {
                Id = l.Id,
                Address = l.Address,
                City = l.City,
                Zipcode = l.Zipcode,
                State = l.State,
                Inventory = l.inventory
            }));
        }
    }
}
