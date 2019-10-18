using System.Collections.Generic;

namespace Project1.BusinessLogic
{
    public interface IRepository
    {
        IEnumerable<Customer> GetAllCustomers();

        void AddCustomer(Customer customer);

        IEnumerable<Customer> GetCustomersByLastName(string lastName);
    }
}
