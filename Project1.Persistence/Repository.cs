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
            return _context.Customer.Select(c => new BusinessLogic.Customer
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName
            });
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
            return _context.Customer.Where(c => c.LastName.ToLower() == lastName.ToLower())
                .Select(c => new BusinessLogic.Customer
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                });
        }

        public IEnumerable<Order> GetOrdersByLocationId(int locationId)
        {
            return _context.Orders.Where(o => o.LocationId == locationId).ToList()
                .Select(o => new BusinessLogic.Order
                {
                    Id = o.Id,
                    StoreLocation = GetLocationById(o.LocationId),
                    Customer = GetCustomerById(o.CustomerId),
                    OrderTime = o.OrderTime,
                    LineItems = GetLineItemsByOrderId(o.Id)
                });
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
