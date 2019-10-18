using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BusinessLogic.IRepository _repository;

        public CustomerController(BusinessLogic.IRepository repository)
        {
            _repository = repository;
        }

        // GET: Customer
        public ActionResult Index()
        {
            IEnumerable<BusinessLogic.Customer> blCustomers = _repository.GetAllCustomers();
            return View(blCustomers.Select(c => new WebApp.Models.Customer
            {
                Id = c.Id.ToString(),
                FirstName = c.FirstName,
                LastName = c.LastName
            }));
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
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

                _repository.AddCustomer(new BusinessLogic.Customer
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(viewModel);
            }
        }

        // GET: Customer/Search
        public ActionResult Search()
        {
            return View();
        }

        // POST: Customer/Search
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
                ModelState.AddModelError("Name", ex.Message);
                return View(viewModel);
            }
        }

        public ActionResult SearchResults(WebApp.Models.CustomerSearch viewModel)
        {
            try
            {
                IEnumerable<BusinessLogic.Customer> filteredCustomers = _repository.GetCustomersByLastName(viewModel.LastName);

                return View("SearchResults", filteredCustomers.Select(c => new WebApp.Models.Customer
                {
                    Id = c.Id.ToString(),
                    FirstName = c.FirstName,
                    LastName = c.LastName
                }));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(viewModel);
            }
        }
    }
}
