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
            try
            {
                BusinessLogic.Order order = _repository.GetOrderById(id);

                return View(new WebApp.Models.OrderDetails
                {
                    Id = order.Id.ToString(),
                    Location = order.StoreLocation.ToString(),
                    Customer = order.Customer.ToString(),
                    OrderTime = order.OrderTime.ToString(),
                    LineItems = order.StoreLocation.ToStringInventory()
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(new WebApp.Models.Order());
            }
        }

        // GET: Order/Place
        public ActionResult Place()
        {
            IEnumerable<BusinessLogic.Location> locations = _repository.GetAllLocations();
            IEnumerable<BusinessLogic.Customer> customers = _repository.GetAllCustomers();
            return View(new WebApp.Models.OrderPlace
            {
                Locations = locations.Select(l => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = l.ToString(),
                    Value = l.Id.ToString()
                }),
                Customers = customers.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = c.ToString(),
                    Value = c.Id.ToString()
                })
            });
        }

        // POST: Order/Place
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WebApp.Models.OrderPlace viewModel)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
