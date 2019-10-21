using System.Collections.Generic;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// The interface for communicating with the persistence layer.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Return all customers.
        /// </summary>
        /// <returns>All customers</returns>
        IEnumerable<Customer> GetAllCustomers();

        /// <summary>
        /// Add a customer with a first and last name.
        /// </summary>
        /// <param name="firstName">First name of the customer to add</param>
        /// <param name="lastName">Last name of the customer to add</param>
        public void AddCustomer(string firstName, string lastName);

        /// <summary>
        /// Get all customers with the given last name.
        /// </summary>
        /// <param name="lastName">Last name to search for</param>
        /// <returns>All customers with the given last name</returns>
        IEnumerable<Customer> GetCustomersByLastName(string lastName);

        /// <summary>
        /// Get all orders of the location.
        /// </summary>
        /// <param name="locationId">The location id to search orders for</param>
        /// <returns>All orders of the location</returns>
        IEnumerable<Order> GetOrdersByLocationId(int locationId);

        /// <summary>
        /// Get all orders of the customer.
        /// </summary>
        /// <param name="customerId">The customer id to search orders for</param>
        /// <returns>All orders of the customer</returns>
        IEnumerable<Order> GetOrdersByCustomerId(int customerId);

        /// <summary>
        /// Get a particular order.
        /// </summary>
        /// <param name="orderId">The id of the order</param>
        /// <returns>The order with the id</returns>
        Order GetOrderById(int orderId);

        /// <summary>
        /// Get all locations.
        /// </summary>
        /// <returns>All locations</returns>
        IEnumerable<Location> GetAllLocations();

        /// <summary>
        /// Get a particular location.
        /// </summary>
        /// <param name="locationId">The location id</param>
        /// <returns>The location with the id</returns>
        Location GetLocationById(int locationId);

        /// <summary>
        /// Get a particular customer.
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <returns>The customer with the id</returns>
        Customer GetCustomerById(int customerId);

        /// <summary>
        /// Get a particular product.
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <returns>The product with the id</returns>
        Product GetProductById(int productId);

        /// <summary>
        /// Create an order.
        /// </summary>
        /// <param name="locationId">The location id</param>
        /// <param name="customerId">The customer id</param>
        /// <param name="selectedInventory">The selected dictionary of product ids to stock</param>
        /// <returns>The id of the newly created order</returns>
        int CreateOrder(int locationId, int customerId, Dictionary<int, int> selectedInventory);
    }
}
