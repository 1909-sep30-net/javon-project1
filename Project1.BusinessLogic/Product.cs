using System;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// The Product object for the Business Logic. Upon creation, validates the product data.
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        /// <summary>
        /// Returns the id, name, and price of the product in string format.
        /// </summary>
        /// <returns>The id, name, and price of the product</returns>
        public override string ToString()
        {
            return $"[Product {Id}] [Name] {Name} [Price] ${Decimal.Round(Price, 2)}";
        }
    }
}
