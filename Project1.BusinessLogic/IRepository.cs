using System.Collections.Generic;

namespace Project1.BusinessLogic
{
    public interface IRepository
    {
        public IEnumerable<Customer> GetAllCustomers();
    }
}
