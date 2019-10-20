using Project1.BusinessLogic;
using Xunit;

namespace Project1.Test
{
    public class BusinessProductTest
    {
        [Theory]
        [InlineData(8, "Winter Chai", 32.16, 8, "Winter Chai", 32.16)]
        [InlineData(4, "Humblestone", 6.32, 4, "Humblestone", 6.32)]
        [InlineData(10032, "Herbal Kliff", 9.99, 10032, "Herbal Kliff", 9.99)]
        public void TestBusinessProductSettersAndGetters(int id, string name, decimal price, int expectedId, string expectedName, decimal expectedPrice)
        {
            Product product = new Product();
            product.Id = id;
            product.Name = name;
            product.Price = price;

            Assert.Equal(expectedId, product.Id);
            Assert.Equal(expectedName, product.Name);
            Assert.Equal(expectedPrice, product.Price);
        }
    }
}
