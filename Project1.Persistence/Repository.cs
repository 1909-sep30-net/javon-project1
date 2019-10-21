using Project1.BusinessLogic;
using Project1.Persistence.Entities;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using System;

namespace Project1.Persistence
{
    /// <summary>
    /// Implementation of the IRepository for persisting data into my SQL Server hosted on Azure.
    /// </summary>
    public class Repository : IRepository
    {
        private readonly TThreeTeasContext _context;

        /// <summary>
        /// Dependency injects the DB Context into this repository.
        /// </summary>
        /// <param name="context">The scaffolded DB Context</param>
        public Repository(TThreeTeasContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return all customers.
        /// </summary>
        /// <returns>All customers</returns>
        public IEnumerable<BusinessLogic.Customer> GetAllCustomers()
        {
            Log.Information($"Getting all customers");
            return _context.Customer
                .Select(c => new BusinessLogic.Customer()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                });
        }

        /// <summary>
        /// Add a customer with a first and last name.
        /// </summary>
        /// <param name="firstName">First name of the customer to add</param>
        /// <param name="lastName">Last name of the customer to add</param>
        public void AddCustomer(string firstName, string lastName)
        {
            Log.Information($"Adding customer {firstName} {lastName}");
            _context.Customer
                .Add(new Entities.Customer
                {
                    FirstName = firstName,
                    LastName = lastName
                });
            _context.SaveChanges();
        }

        /// <summary>
        /// Get all customers with the given last name.
        /// </summary>
        /// <param name="lastName">Last name to search for</param>
        /// <returns>All customers with the given last name</returns>
        public IEnumerable<BusinessLogic.Customer> GetCustomersByLastName(string lastName)
        {
            Log.Information($"Getting all customers with last name {lastName}");
            return _context.Customer
                .Where(c => c.LastName.ToLower() == lastName.ToLower())
                .Select(c => new BusinessLogic.Customer()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                });
        }

        /// <summary>
        /// Get all orders of the location.
        /// </summary>
        /// <param name="locationId">The location id to search orders for</param>
        /// <returns>All orders of the location</returns>
        public IEnumerable<Order> GetOrdersByLocationId(int locationId)
        {
            Log.Information($"Getting all orders with location ID {locationId}");
            return _context.Orders.Where(o => o.LocationId == locationId).ToList()
                .Select(o => GetOrderById(o.Id));
        }

        /// <summary>
        /// Get all orders of the customer.
        /// </summary>
        /// <param name="customerId">The customer id to search orders for</param>
        /// <returns>All orders of the customer</returns>
        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            Log.Information($"Getting all orders with customer ID {customerId}");
            return _context.Orders.Where(o => o.CustomerId == customerId).ToList()
                .Select(o => GetOrderById(o.Id));
        }

        /// <summary>
        /// Get a particular order.
        /// </summary>
        /// <param name="orderId">The id of the order</param>
        /// <returns>The order with the id</returns>
        public BusinessLogic.Order GetOrderById(int orderId)
        {
            Log.Information($"Getting order with ID {orderId}");
            Entities.Orders order = _context.Orders.Where(o => o.Id == orderId).FirstOrDefault();
            if (order is null)
            {
                throw new OrderException("Invalid order ID");
            }

            return new BusinessLogic.Order
            {
                Id = order.Id,
                StoreLocation = GetLocationById(order.LocationId),
                Customer = GetCustomerById(order.CustomerId),
                OrderTime = order.OrderTime,
                LineItems = GetLineItemsByOrderId(order.Id)
            };
        }

        /// <summary>
        /// Get all locations.
        /// </summary>
        /// <returns>All locations</returns>
        public IEnumerable<BusinessLogic.Location> GetAllLocations()
        {
            Log.Information($"Getting all locations");
            return _context.Location.ToList().Select(l => GetLocationById(l.Id));
        }

        /// <summary>
        /// Get a particular location.
        /// </summary>
        /// <param name="locationId">The location id</param>
        /// <returns>The location with the id</returns>
        public BusinessLogic.Location GetLocationById(int locationId)
        {
            Log.Information($"Getting location with ID {locationId}");
            Entities.Location location = _context.Location.Where(l => l.Id == locationId).FirstOrDefault();
            if (location is null)
            {
                throw new LocationException("Invalid location ID");
            }
            BusinessLogic.Location bLocation = new BusinessLogic.Location()
            {
                Id = location.Id,
                Address = location.Address,
                City = location.City,
                Zipcode = location.Zipcode,
                State = location.State
            };

            IEnumerable<Inventory> inventories = _context.Inventory.Where(i => i.LocationId == location.Id).ToList();
            foreach (Inventory inventory in inventories)
            {
                bLocation.AddProduct(GetProductById(inventory.ProductId), inventory.Stock);
            }
            return bLocation;
        }

        /// <summary>
        /// Get a particular customer.
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <returns>The customer with the id</returns>
        public BusinessLogic.Customer GetCustomerById(int customerId)
        {
            Log.Information($"Getting customer with ID {customerId}");
            Entities.Customer customer = _context.Customer.Where(c => c.Id == customerId).FirstOrDefault();
            if (customer is null)
            {
                throw new CustomerException("Invalid customer ID");
            }
            return new BusinessLogic.Customer()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
        }

        /// <summary>
        /// Get a particular product.
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <returns>The product with the id</returns>
        public BusinessLogic.Product GetProductById(int productId)
        {
            Log.Information($"Getting product with ID {productId}");
            Entities.Product product = _context.Product.Where(p => p.Id == productId).FirstOrDefault();
            if (product is null)
            {
                throw new ProductException("Invalid product ID");
            }
            return new BusinessLogic.Product()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        /// <summary>
        /// Create an order.
        /// </summary>
        /// <param name="locationId">The location id</param>
        /// <param name="customerId">The customer id</param>
        /// <param name="selectedInventory">The selected dictionary of product ids to stock</param>
        public void CreateOrder(int locationId, int customerId, Dictionary<int, int> selectedInventory)
        {
            Log.Information($"Creating order with location ID {locationId} and customer ID {customerId}");
            BusinessLogic.Order order = new BusinessLogic.Order
            {
                StoreLocation = GetLocationById(locationId),
                Customer = GetCustomerById(customerId),
                OrderTime = DateTime.Now
            };
            Dictionary<BusinessLogic.Product, int> inv = new Dictionary<BusinessLogic.Product, int>();
            foreach (KeyValuePair<int, int> item in selectedInventory)
            {
                if (item.Value > 0)
                {
                    BusinessLogic.Product product = GetProductById(item.Key);
                    order.StoreLocation.DecrementStock(product, item.Value);
                    inv.Add(product, item.Value);
                }
            }
            order.AddLineItems(inv);

            Orders additionalOrder = new Orders()
            {
                LocationId = order.StoreLocation.Id,
                CustomerId = order.Customer.Id,
                OrderTime = order.OrderTime
            };

            _context.Orders.Add(additionalOrder);
            _context.SaveChanges();
            Log.Information($"Added the additional order to the database");

            List<LineItem> additionalLineItems = new List<LineItem>();
            foreach (KeyValuePair<BusinessLogic.Product, int> lineItem in order.LineItems)
            {
                LineItem additionalLineItem = new LineItem()
                {
                    OrdersId = additionalOrder.Id,
                    ProductId = lineItem.Key.Id,
                    Quantity = lineItem.Value
                };
                additionalLineItems.Add(additionalLineItem);
            }
            foreach (LineItem additionalLineItem in additionalLineItems)
            {
                _context.LineItem.Add(additionalLineItem);
            }
            _context.SaveChanges();
            Log.Information($"Added the additional line items to the database");

            foreach (KeyValuePair<BusinessLogic.Product, int> inventory in order.StoreLocation.inventory)
            {
                Log.Information($"inventory item - Location ID {order.StoreLocation.Id} {inventory.Key} Stock {inventory.Value}");
                Inventory updatedInventory = _context.Inventory
                    .Where(i => (i.LocationId == order.StoreLocation.Id && i.ProductId == inventory.Key.Id))
                    .FirstOrDefault();
                updatedInventory.Stock = inventory.Value;
            }

            _context.SaveChanges();
            Log.Information($"Updated inventories to the database");
        }

        /// <summary>
        /// Gets the line items of an order.
        /// </summary>
        /// <param name="orderId">The id of the order</param>
        /// <returns>The line items of the order</returns>
        private Dictionary<BusinessLogic.Product, int> GetLineItemsByOrderId(int orderId)
        {
            Log.Information($"Getting line items of order ID {orderId}");
            Dictionary<BusinessLogic.Product, int> lineItems = new Dictionary<BusinessLogic.Product, int>();
            IEnumerable<LineItem> lineItemList = _context.LineItem.Where(i => i.OrdersId == orderId).ToList();
            foreach (LineItem lineItem in lineItemList)
            {
                lineItems.Add(GetProductById(lineItem.ProductId), lineItem.Quantity);
            }
            return lineItems;
        }
    }
}
