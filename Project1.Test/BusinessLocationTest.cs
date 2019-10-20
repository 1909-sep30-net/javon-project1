using Project1.BusinessLogic;
using Xunit;

namespace Project1.Test
{
    public class BusinessLocationTest
    {
        [Theory]
        [InlineData(5, "467 East Eagle Rd.", "Tallahassee", "32303", "FL", 5, "467 East Eagle Rd.", "Tallahassee", "32303", "FL")]
        [InlineData(32, "148 Iroquois Lane", "Elizabethton", "37643", "TN", 32, "148 Iroquois Lane", "Elizabethton", "37643", "TN")]
        [InlineData(926, "415 Princess St.", "Pembroke Pines", "33028", "FL", 926, "415 Princess St.", "Pembroke Pines", "33028", "FL")]
        public void TestBusinessLocationSettersAndGetters(int id, string address, string city, string zipcode, string state,
            int expectedId, string expectedAddress, string expectedCity, string expectedZipcode, string expectedState)
        {
            Location location = new Location();
            location.Id = id;
            location.Address = address;
            location.City = city;
            location.Zipcode = zipcode;
            location.State = state;

            Assert.Equal(expectedId, id);
            Assert.Equal(expectedAddress, address);
            Assert.Equal(expectedCity, city);
            Assert.Equal(expectedZipcode, zipcode);
            Assert.Equal(expectedState, state);
        }

        [Theory]
        [InlineData(5, "467 East Eagle Rd.", "Tallahassee", "32303", "FL", 2, "White Winter Chai", 72.62, 4)]
        [InlineData(32, "148 Iroquois Lane", "Elizabethton", "37643", "TN", 63, "Greenland Bliss", 30.16, 16)]
        [InlineData(926, "415 Princess St.", "Pembroke Pines", "33028", "FL", 15, "Red Chai Spice", 5.62, 7)]
        public void TestBusinessLocationAddProduct(int id, string address, string city, string zipcode, string state,
            int productId, string productName, decimal productPrice, int productStock)
        {
            Location location = new Location();
            location.Id = id;
            location.Address = address;
            location.City = city;
            location.Zipcode = zipcode;
            location.State = state;

            Product product = new Product()
            {
                Id = productId,
                Name = productName,
                Price = productPrice
            };
            location.AddProduct(product, productStock);

            Assert.True(location.inventory.ContainsKey(product));
            Assert.Equal(location.inventory[product], productStock);
        }

        [Theory]
        [InlineData(5, "467 East Eagle Rd.", "Tallahassee", "32303", "FL", 2, "White Winter Chai", 72.62, 4)]
        [InlineData(32, "148 Iroquois Lane", "Elizabethton", "37643", "TN", 63, "Greenland Bliss", 30.16, 16)]
        [InlineData(926, "415 Princess St.", "Pembroke Pines", "33028", "FL", 15, "Red Chai Spice", 5.62, 7)]
        public void TestBusinessLocationDecrementStock(int id, string address, string city, string zipcode, string state,
int productId, string productName, decimal productPrice, int productStock)
        {
            Location location = new Location();
            location.Id = id;
            location.Address = address;
            location.City = city;
            location.Zipcode = zipcode;
            location.State = state;

            Product product = new Product()
            {
                Id = productId,
                Name = productName,
                Price = productPrice
            };
            location.AddProduct(product, productStock);
            int quantity = 1;

            location.DecrementStock(product, quantity);

            Assert.Equal(location.inventory[product], productStock - 1);
        }

        [Theory]
        [InlineData(5, "467 East Eagle Rd.", "Tallahassee", "32303", "FL", 2, "White Winter Chai", 72.62, 4)]
        [InlineData(32, "148 Iroquois Lane", "Elizabethton", "37643", "TN", 63, "Greenland Bliss", 30.16, 16)]
        [InlineData(926, "415 Princess St.", "Pembroke Pines", "33028", "FL", 15, "Red Chai Spice", 5.62, 7)]
        public void TestBusinessLocationDecrementStockWhenProductNotInStock(int id, string address, string city, string zipcode, string state,
    int productId, string productName, decimal productPrice, int productStock)
        {
            Location location = new Location();
            location.Id = id;
            location.Address = address;
            location.City = city;
            location.Zipcode = zipcode;
            location.State = state;

            Product product = new Product()
            {
                Id = productId,
                Name = productName,
                Price = productPrice
            };
            location.AddProduct(product, productStock);

            Product productNotInStock = new Product()
            {
                Id = productId + 1,
                Name = productName + "a",
                Price = productPrice + 1
            };
            int quantity = 10;

            Assert.Throws<LocationException>(() => location.DecrementStock(productNotInStock, quantity));
        }

        [Theory]
        [InlineData(5, "467 East Eagle Rd.", "Tallahassee", "32303", "FL", 2, "White Winter Chai", 72.62, 4)]
        [InlineData(32, "148 Iroquois Lane", "Elizabethton", "37643", "TN", 63, "Greenland Bliss", 30.16, 16)]
        [InlineData(926, "415 Princess St.", "Pembroke Pines", "33028", "FL", 15, "Red Chai Spice", 5.62, 7)]
        public void TestBusinessLocationDecrementStockWhenProductQuantityGreaterThanStock(int id, string address, string city, string zipcode, string state,
int productId, string productName, decimal productPrice, int productStock)
        {
            Location location = new Location();
            location.Id = id;
            location.Address = address;
            location.City = city;
            location.Zipcode = zipcode;
            location.State = state;

            Product product = new Product()
            {
                Id = productId,
                Name = productName,
                Price = productPrice
            };
            location.AddProduct(product, productStock);
            int quantity = productStock + 1;

            Assert.Throws<LocationException>(() => location.DecrementStock(product, quantity));
        }
    }
}
