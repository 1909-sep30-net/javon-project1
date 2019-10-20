﻿using System.Collections.Generic;

namespace Project1.BusinessLogic
{
    public interface IRepository
    {
        IEnumerable<Customer> GetAllCustomers();

        void AddCustomer(Customer customer);

        IEnumerable<Customer> GetCustomersByLastName(string lastName);

        IEnumerable<Order> GetOrdersByLocationId(int locationId);

        IEnumerable<Order> GetOrdersByCustomerId(int customerId);

        IEnumerable<Location> GetAllLocations();

        Order GetOrderById(int id);

        Location GetLocationById(int locationId);

        Customer GetCustomerById(int customerId);

        Product GetProductById(int productId);

        void CreateOrder(int locationId, int customerId, Dictionary<int, int> updatedInventories);
    }
}
