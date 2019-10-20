using Project1.BusinessLogic;
using Xunit;

namespace Project1.Test
{
    public class BusinessOrderTest
    {
        [Theory]
        [InlineData(9)]
        [InlineData(81)]
        [InlineData(10672615)]
        public void TestBusinessOrderSettersAndGetters(int id)
        {
            Order order = new Order();
            order.Id = id;

            Assert.Equal(id, order.Id);
        }
    }
}
