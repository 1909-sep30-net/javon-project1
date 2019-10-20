using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;

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
            Log.Information($"Placing an Order GET");
            IEnumerable<BusinessLogic.Location> locations = _repository.GetAllLocations();
            IEnumerable<BusinessLogic.Customer> customers = _repository.GetAllCustomers();
            return View(new WebApp.Models.OrderPlace
            {
                Locations = locations,
                Customers = customers
            });
        }

        // POST: Order/Place
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Place(WebApp.Models.OrderPlace viewModel)
        {
            Log.Information($"Placing an Order POST - Redirecting to GET Place2");
            try
            {
                return RedirectToAction(nameof(Place2), viewModel);
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Place2
        public ActionResult Place2(WebApp.Models.OrderPlace viewModel)
        {
            Log.Information($"Placing an Order 2 - Location ID: {viewModel.LocationId} Customer ID: {viewModel.CustomerId}");
            BusinessLogic.Location location = _repository.GetLocationById(viewModel.LocationId);
            Log.Information($"Placing an Order 2 - Location: {location.ToString()} Inventory: {location.ToStringInventory()}");
            return View(new WebApp.Models.OrderCreate
            {
                LocationId = viewModel.LocationId,
                CustomerId = viewModel.CustomerId,
                Inventory = location.inventory,
                SelectedInventory = location.inventory.ToDictionary(i => i.Key.Id, i => 0)
            });
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WebApp.Models.OrderCreate viewModel)
        {
            try
            {
                Log.Information($"Creating an order - location ID {viewModel.LocationId} for customer ID {viewModel.CustomerId}");
                foreach (KeyValuePair<int, int> item in viewModel.SelectedInventory)
                {
                    Log.Information($"Product ID {item.Key} Quantity {item.Value}");
                }

                _repository.CreateOrder(
                    viewModel.LocationId,
                    viewModel.CustomerId,
                    viewModel.SelectedInventory);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
