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
    }
}
