using Project1.BusinessLogic;
using Project1.Persistence.Entities;
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
    }
}
