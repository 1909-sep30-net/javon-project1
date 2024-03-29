﻿using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// GET: Order
        /// </summary>
        /// <returns>The links for performing order operations</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: Order/LocationSearch
        /// </summary>
        /// <returns>
        /// The form for searching the order history of a location, otherwise the index if there was
        /// an exception
        /// </returns>
        public ActionResult LocationSearch()
        {
            try
            {
                return View(new WebApp.Models.OrderLocationSearch
                {
                    Locations = _repository.GetAllLocations(),
                    LocationId = 0
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// POST: Order/LocationSearch
        /// </summary>
        /// <param name="viewModel">The form for searching the order history of a location</param>
        /// <returns>Redirects to the location history page otherwise index if there was an exception</returns>
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
                Log.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// GET: Order/LocationHistory/
        /// </summary>
        /// <param name="viewModel">The form for searching the order history of a location</param>
        /// <returns>
        /// The list of orders of that location otherwise the index if there was an exception
        /// </returns>
        public ActionResult LocationHistory(WebApp.Models.OrderLocationSearch viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(_repository.GetOrdersByLocationId(viewModel.LocationId)
                    .Select(o => new WebApp.Models.Order
                    {
                        Id = o.Id.ToString(),
                        Location = o.StoreLocation.ToString(),
                        Customer = o.Customer.ToString(),
                        OrderTime = o.OrderTime.ToString()
                    }));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// GET: Order/CustomerSearch
        /// </summary>
        /// <returns>
        /// Returns the form for searching orders of a customer, otherwise the index if there was an exception
        /// </returns>
        public ActionResult CustomerSearch()
        {
            try
            {
                return View(new WebApp.Models.OrderCustomerSearch
                {
                    Customers = _repository.GetAllCustomers(),
                    CustomerId = 0
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// POST: Order/CustomerSearch
        /// </summary>
        /// <param name="viewModel">The form for searching orders of a customer</param>
        /// <returns>
        /// Redirects to the customer history page otherwise the index if there was an exception
        /// </returns>
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
                Log.Error(ex.Message);
                return View(viewModel);
            }
        }

        /// <summary>
        /// GET: Order/CustomerHistory/
        /// </summary>
        /// <param name="viewModel">The form for searching orders of a customer</param>
        /// <returns>The order history of the customer otherwise the index if there was an exception</returns>
        public ActionResult CustomerHistory(WebApp.Models.OrderCustomerSearch viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(_repository.GetOrdersByCustomerId(viewModel.CustomerId)
                    .Select(o => new WebApp.Models.Order
                    {
                        Id = o.Id.ToString(),
                        Location = o.StoreLocation.ToString(),
                        Customer = o.Customer.ToString(),
                        OrderTime = o.OrderTime.ToString()
                    }));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// GET: Order/Details/{id}
        /// </summary>
        /// <param name="id">The order id</param>
        /// <returns>The details of the order otherwise the index if there was an exception</returns>
        public ActionResult Details(int id)
        {
            try
            {
                Log.Information($"Getting details of order ID {id}");
                BusinessLogic.Order order = _repository.GetOrderById(id);
                return View(new WebApp.Models.OrderDetails
                {
                    Id = order.Id.ToString(),
                    Location = order.StoreLocation.ToString(),
                    Customer = order.Customer.ToString(),
                    OrderTime = order.OrderTime.ToString(),
                    LineItems = order.LineItems,
                    TotalSale = order.Total
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// GET: Order/Place
        /// </summary>
        /// <returns>
        /// The first form for placing an order otherwise the index if there was an exception
        /// </returns>
        public ActionResult Place()
        {
            try
            {
                Log.Information($"Placing an Order GET");
                return View(new WebApp.Models.OrderPlace
                {
                    Locations = _repository.GetAllLocations(),
                    Customers = _repository.GetAllCustomers()
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// POST: Order/Place
        /// </summary>
        /// <param name="viewModel">The first form for placing an order</param>
        /// <returns>
        /// Redirects to getting the second form, otherwise the index if there was an exception
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Place(WebApp.Models.OrderPlace viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                Log.Information($"Placing an Order POST - Redirecting to GET Place2");
                return RedirectToAction(nameof(Place2), viewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// GET: Order/Place2
        /// </summary>
        /// <param name="viewModel">The first form input</param>
        /// <returns>
        /// The second form for placing an order, otherwise the index if there was an exception
        /// </returns>
        public ActionResult Place2(WebApp.Models.OrderPlace viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
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
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// POST: Order/Create
        /// </summary>
        /// <param name="viewModel">The second form for creating an order</param>
        /// <returns>Creates an order and redirects to the index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WebApp.Models.OrderCreate viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                Log.Information($"Creating an order - location ID {viewModel.LocationId} for customer ID {viewModel.CustomerId}");
                foreach (KeyValuePair<int, int> item in viewModel.SelectedInventory)
                {
                    Log.Information($"Product ID {item.Key} Quantity {item.Value}");
                }
                int additionalOrderId = _repository.CreateOrder(
                    viewModel.LocationId,
                    viewModel.CustomerId,
                    viewModel.SelectedInventory);
                Log.Information($"Received order ID {additionalOrderId} from CreateOrder");
                return RedirectToAction(nameof(Details), new { id = additionalOrderId });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
