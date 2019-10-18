using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project1.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly BusinessLogic.IRepository _repository;

        public OrderController(BusinessLogic.IRepository repository)
        {
            _repository = repository;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        // GET: Order/LocationSearch
        public ActionResult LocationSearch()
        {
            return View();
        }

        // POST: Order/LocationSearch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LocationSearch(WebApp.Models.OrderLocationSearch viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                return RedirectToAction(nameof(LocationHistory), viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(viewModel);
            }
        }

        // GET: Order/LocationHistory/
        public ActionResult LocationHistory(WebApp.Models.OrderLocationSearch viewModel)
        {
            try
            {
                IEnumerable<BusinessLogic.Order> filteredOrders = _repository.GetOrdersByLocationId(viewModel.LocationId);

                return View("LocationHistory", filteredOrders.Select(o => new WebApp.Models.Order
                {
                    Id = o.Id.ToString(),
                    LocationId = o.StoreLocation.Id.ToString(),
                    CustomerId = o.Customer.Id.ToString(),
                    OrderTime = o.OrderTime.ToString(),
                    LineItems = o.StoreLocation.ToStringInventory()
                }));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(viewModel);
            }
        }

        // GET: Order/CustomerSearch
        public ActionResult CustomerSearch()
        {
            return View();
        }

        // GET: Order/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Order/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Order/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
