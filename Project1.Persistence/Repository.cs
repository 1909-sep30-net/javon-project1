using Project1.BusinessLogic;
using Project1.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.Persistence
{
    public class Repository : IRepository
    {
        private readonly TThreeTeasContext _context;

        public Repository(TThreeTeasContext context)
        {
            _context = context;
        }

        public IEnumerable<BusinessLogic.Customer> GetAllCustomers()
        {
            return _context.Customer.ToList().Select(c => GetCustomerById(c.Id));
        }

        public void AddCustomer(BusinessLogic.Customer customer)
        {
            _context.Customer.Add(new Entities.Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName
            });
            _context.SaveChanges();
        }

        public IEnumerable<BusinessLogic.Customer> GetCustomersByLastName(string lastName)
        {
            return _context.Customer.Where(c => c.LastName.ToLower() == lastName.ToLower()).ToList()
                .Select(c => GetCustomerById(c.Id));
        }

        public IEnumerable<Order> GetOrdersByLocationId(int locationId)
        {
            return _context.Orders.Where(o => o.LocationId == locationId).ToList()
                .Select(o => GetOrderById(o.Id));
        }

        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            return _context.Orders.Where(o => o.CustomerId == customerId).ToList()
                .Select(o => GetOrderById(o.Id));
        }

        public BusinessLogic.Order GetOrderById(int id)
        {
            Entities.Orders order = _context.Orders.Where(o => o.Id == id).FirstOrDefault();
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

        public IEnumerable<BusinessLogic.Location> GetAllLocations()
        {
            return _context.Location.ToList().Select(l => GetLocationById(l.Id));
        }

        private BusinessLogic.Location GetLocationById(int locationId)
        {
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

        private BusinessLogic.Product GetProductById(int productId)
        {
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

        private BusinessLogic.Customer GetCustomerById(int customerId)
        {
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

        private Dictionary<BusinessLogic.Product, int> GetLineItemsByOrderId(int orderId)
        {
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
