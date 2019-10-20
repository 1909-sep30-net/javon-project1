using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace Project1.BusinessLogic
{
    /// <summary>
    /// The Location object for the Business Logic. Upon creation, validates the location data and
    /// allows methods to add products to the inventory and decrement from the inventory with constraints.
    /// </summary>
    public class Location
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string State { get; set; }
        public Dictionary<Product, int> inventory = new Dictionary<Product, int>();

        /// <summary>
        /// Adds a product to this location's inventory.
        /// </summary>
        /// <param name="bProduct">The product to add</param>
        /// <param name="stock">The quantity of the product to add</param>
        public void AddProduct(Product bProduct, int stock)
        {
            inventory.Add(bProduct, stock);
        }

        /// <summary>
        /// Returns the location ID, address, city, state, and zip code in string format
        /// </summary>
        /// <returns>The location ID, address, city, state, and zip code</returns>
        public override string ToString()
        {
            return $"[Location {Id}] {Address}, {City}, {State}, {Zipcode}";
        }

        /// <summary>
        /// Returns the each product and stock in the inventory of this location in string format
        /// </summary>
        /// <returns>Each product and stock in the inventory of this location</returns>
        public string ToStringInventory()
        {
            string str = $"";
            foreach (KeyValuePair<Product, int> item in inventory)
            {
                str += $"{item.Key} [Quantity] {item.Value}\n";
            }
            return str;
        }

        /// <summary>
        /// Decrements the stock of the inventory of this location.
        /// </summary>
        /// <param name="bProduct">The product to decrement</param>
        /// <param name="qty">The amount of the product to decrement</param>
        public void DecrementStock(Product bProduct, int qty)
        {
            Log.Information($"Decrementing stock of location {Id} of product {bProduct} with quantity {qty}");
            if (!inventory.Keys.Any(p => p.Id == bProduct.Id))
            {
                throw new LocationException($"[!] Location does not have {bProduct} in stock");
            }

            Product selectedProduct = inventory.Keys.Where(p => p.Id == bProduct.Id).FirstOrDefault();

            if (qty > inventory[selectedProduct])
            {
                throw new LocationException($"[!] Location {Id} does not have {selectedProduct} with {qty} stock, only has {inventory[selectedProduct]} in stock");
            }
            inventory[selectedProduct] -= qty;
        }
    }
}
