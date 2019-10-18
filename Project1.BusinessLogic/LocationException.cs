using System;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// Exception related to the BusinessLocation class.
    /// </summary>
    public class LocationException : Exception
    {
        /// <summary>
        /// Base exception constructor
        /// </summary>
        /// <param name="message">Message of the exception</param>
        public LocationException(string message) : base(message)
        {
        }
    }
}
