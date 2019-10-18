using System;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// Exception related to the BusinessCustomer class.
    /// </summary>
    public class CustomerException : Exception
    {
        /// <summary>
        /// Base exception constructor
        /// </summary>
        /// <param name="message">Message of the exception</param>
        public CustomerException(string message) : base(message)
        {
        }
    }
}
