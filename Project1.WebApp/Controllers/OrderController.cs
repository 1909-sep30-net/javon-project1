using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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

                return View(filteredOrders.Select(o => new WebApp.Models.Order
                {
                    Id = o.Id.ToString(),
                    Location = o.StoreLocation.ToString(),
                    Customer = o.Customer.ToString(),
                    OrderTime = o.OrderTime.ToString()
                }));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(new List<WebApp.Models.Order>());
            }
        }

        // GET: Order/CustomerSearch
        public ActionResult CustomerSearch()
        {
            return View();
        }

        // POST: Order/CustomerSearch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerSearch(WebApp.Models.OrderCustomerSearch viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                return RedirectToAction(nameof(CustomerHistory), viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(viewModel);
            }
        }

        // GET: Order/CustomerHistory/
        public ActionResult CustomerHistory(WebApp.Models.OrderCustomerSearch viewModel)
        {
            try
            {
                IEnumerable<BusinessLogic.Order> filteredOrders = _repository.GetOrdersByCustomerId(viewModel.CustomerId);

                return View(filteredOrders.Select(o => new WebApp.Models.Order
                {
                    Id = o.Id.ToString(),
                    Location = o.StoreLocation.ToString(),
                    Customer = o.Customer.ToString(),
                    OrderTime = o.OrderTime.ToString()
                }));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(new List<WebApp.Models.Order>());
            }
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

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
