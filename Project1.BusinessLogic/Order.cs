using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// The Order object for the Business Logic. Upon creation, validates the order data and allows
    /// methods to add lineitems to the order and validates these line items.
    /// </summary>
    public class Order
    {
        private const int maxQuantity = 20;
        private const int maxLines = 30;
        public int Id { get; set; }
        public Location StoreLocation { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderTime { get; set; }
        public Dictionary<Product, int> LineItems { get; set; } = new Dictionary<Product, int>();

        /// <summary>
        /// Returns the total sale of the order.
        /// </summary>
        public decimal Total
        {
            get
            {
                decimal sum = 0;
                foreach (var li in LineItems) sum += li.Key.Price * li.Value;
                return sum;
            }
        }

        /// <summary>
        /// Validates that the order does not have too many line items. Throws an exception if it does.
        /// </summary>
        private void ValidateNotTooManyLines()
        {
            Log.Information($"Validating not too many lines");
            if (LineItems.Count > maxLines)
            {
                throw new OrderException($"[!] Too many lines for this order");
            }
        }

        /// <summary>
        /// Validates that the order does not have a duplicate product ID in the line items. Throws
        /// an exception if it does.
        /// </summary>
        /// <param name="productId"></param>
        private void ValidateNotDuplicateProductId(int productId)
        {
            Log.Information($"Validating {productId} for duplicate product");
            if (LineItems.Any(l => l.Key.Id == productId))
            {
                throw new OrderException($"[!] Duplicate product Id {productId}");
            }
        }

        /// <summary>
        /// Validates the quantity of the line item is greater than zero. Throws an exception if it
        /// is not.
        /// </summary>
        /// <param name="product">The product of the line item to validate</param>
        /// <param name="qty">The quantity of the line item</param>
        private void ValidateQuantityGreaterThanZero(Product product, int qty)
        {
            Log.Information($"Validating {product} with quantity {qty} has quantity greater than zero");
            if (qty <= 0)
            {
                throw new OrderException($"[!] {product} of quantity {qty} item does not have a quantity greater than 0");
            }
        }

        /// <summary>
        /// Validates that the quantity of the line item is less than the limit. Throws an exception
        /// if it is not.
        /// </summary>
        /// <param name="product">The product of the line item to validate</param>
        /// <param name="qty">The quantity of the line item</param>
        private void ValidateQuantityBelowLimit(Product product, int qty)
        {
            Log.Information($"Validating {product} with quantity {qty} has quantity below {maxQuantity}");
            if (qty > maxQuantity)
            {
                throw new OrderException($"[!] {product} of quantity {qty} item has a quantity greater than {maxQuantity}");
            }
        }

        /// <summary>
        /// Validates that the order has at least one line. Throws an exception if it does not.
        /// </summary>
        private void ValidateHasLines()
        {
            Log.Information($"Validating order has lines");
            if (LineItems.Count == 0)
            {
                throw new OrderException($"[!] Order has no lines");
            }
        }

        /// <summary>
        /// Adds a list of line items to this order and validates these line items.
        /// </summary>
        /// <param name="lineItems">The list of line items</param>
        public void AddLineItems(Dictionary<Product, int> lineItems)
        {
            Log.Information($"Adding line items {lineItems}");
            foreach (KeyValuePair<Product, int> lineItem in lineItems)
            {
                ValidateNotTooManyLines();
                ValidateNotDuplicateProductId(lineItem.Key.Id);
                ValidateQuantityGreaterThanZero(lineItem.Key, lineItem.Value);
                ValidateQuantityBelowLimit(lineItem.Key, lineItem.Value);
                LineItems.Add(lineItem.Key, lineItem.Value);
            }
            ValidateHasLines();
        }

        /// <summary>
        /// Returns the id, store location, customer, order time, line items, and sale total in
        /// string format.
        /// </summary>
        /// <returns>The id, store location, customer, order time, line items, and sale total</returns>
        public override string ToString()
        {
            string header = $"[Order {Id}]\n" +
                            $"{StoreLocation}\n" +
                            $"{Customer}\n" +
                            $"[Datetime] {OrderTime}\n";
            string footer = $"Sale Total: ${Decimal.Round(Total, 2)}";
            return $"{header}{ToStringLineItems()}{footer}";
        }

        /// <summary>
        /// Returns the line items of this order in string format.
        /// </summary>
        /// <returns>The line items of this order in string format</returns>
        public string ToStringLineItems()
        {
            string str = "";
            foreach (var li in LineItems)
            {
                str += $"{li.Key} [Quantity] {li.Value}\n";
            }
            return str;
        }
    }
}
