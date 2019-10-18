using System;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// Exception related to the BusinessOrder class.
    /// </summary>
    public class OrderException : Exception
    {
        /// <summary>
        /// Base exception constructor
        /// </summary>
        /// <param name="message">Message of the exception</param>
        public OrderException(string message) : base(message)
        {
        }
    }
}
