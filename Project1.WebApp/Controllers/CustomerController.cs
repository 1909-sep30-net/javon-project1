using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.WebApp.Controllers
{
    /// <summary>
    /// Handles the customer requests.
    /// </summary>
    public class CustomerController : Controller
    {
        private readonly BusinessLogic.IRepository _repository;

        public CustomerController(BusinessLogic.IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GET: Customer
        /// </summary>
        /// <returns>List of all customers</returns>
        public ActionResult Index()
        {
            IEnumerable<BusinessLogic.Customer> customers = _repository.GetAllCustomers();
            return View(customers.Select(c => new WebApp.Models.Customer
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName
            }));
        }

        /// <summary>
        /// GET: Customer/Create
        /// </summary>
        /// <returns>Form for creating a customer</returns>
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// POST: Customer/Create
        /// </summary>
        /// <param name="viewModel">The customer form</param>
        /// <returns>The form if it is invalid, otherwise redirect to index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WebApp.Models.Customer viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                BusinessLogic.Customer customer = new BusinessLogic.Customer
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName
                };
                _repository.AddCustomer(customer.FirstName, customer.LastName);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return View(viewModel);
            }
        }

        /// <summary>
        /// GET: Customer/Search
        /// </summary>
        /// <returns>The form for searching a customer by last name</returns>
        public ActionResult Search()
        {
            return View();
        }

        /// <summary>
        /// POST: Customer/Search
        /// </summary>
        /// <param name="viewModel">The form for customer being searched</param>
        /// <returns>The form if it is invalid, otherwise redirect to the search results</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(WebApp.Models.CustomerSearch viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                return RedirectToAction(nameof(SearchResults), viewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return View(viewModel);
            }
        }

        /// <summary>
        /// GET: Customer/SearchResults/
        /// </summary>
        /// <param name="viewModel">The form for customer being searched</param>
        /// <returns>The search results otherwise the index if there was an error</returns>
        public ActionResult SearchResults(WebApp.Models.CustomerSearch viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View("SearchResults", _repository.GetCustomersByLastName(viewModel.LastName)
                    .Select(c => new WebApp.Models.Customer
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName
                    }));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
