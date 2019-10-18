using System;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// Exception related to the BusinessProduct class.
    /// </summary>
    public class ProductException : Exception
    {
        /// <summary>
        /// Base exception constructor
        /// </summary>
        /// <param name="message">Message of the exception</param>
        public ProductException(string message) : base(message)
        {
        }
    }
}
