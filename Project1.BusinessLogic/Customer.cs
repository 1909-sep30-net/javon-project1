using System;
using System.Linq;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// The Customer object for the Business Logic. Upon creation, validates the customer data.
    /// </summary>
    public class Customer
    {
        public const int maxNameLength = 30;
        private string firstName;
        private string lastName;

        public int Id { get; set; }

        /// <summary>
        /// When set, validates the customer's first name and capitalizes it.
        /// </summary>
        public string FirstName
        {
            get => firstName;
            set
            {
                ValidateCustomerFirstName(value);
                firstName = char.ToUpper(value[0]) + value.Substring(1).ToLower();
            }
        }

        /// <summary>
        /// When set, validates the customer's last name and capitalizes it.
        /// </summary>
        public string LastName
        {
            get => lastName;
            set
            {
                ValidateCustomerLastName(value);
                lastName = char.ToUpper(value[0]) + value.Substring(1).ToLower();
            }
        }

        /// <summary>
        /// Validates that the customer's first name is not empty, not too long, and is alphabetical.
        /// </summary>
        /// <param name="first">First name of the customer</param>
        private void ValidateCustomerFirstName(string first)
        {
            if (first.Length == 0) throw new CustomerException("[!] First name is empty");
            else if (first.Length > maxNameLength) throw new CustomerException($"[!] First name is longer than {maxNameLength} characters");
            else if (!first.All(Char.IsLetter)) throw new CustomerException("[!] First name is not alphabetical");
        }

        /// <summary>
        /// Validates that the customer's last name is not empty, not too long, and is alphabetical.
        /// </summary>
        /// <param name="last">Last name of the customer</param>
        private void ValidateCustomerLastName(string last)
        {
            if (last.Length == 0) throw new CustomerException("[!] Last name is empty");
            else if (last.Length > maxNameLength) throw new CustomerException($"[!] Last name is longer than {maxNameLength} characters");
            else if (!last.All(Char.IsLetter)) throw new CustomerException("[!] Last name is not alphabetical");
        }

        /// <summary>
        /// Returns the customer id, first name, and last name in string format
        /// </summary>
        /// <returns>The customer id, first name, and last name in string format</returns>
        public override string ToString()
        {
            return $"[Customer {Id}] {FirstName} {LastName}";
        }
    }
}
