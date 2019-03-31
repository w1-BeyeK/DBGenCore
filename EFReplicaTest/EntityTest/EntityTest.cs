using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EFReplicaTest.EntityTest
{
    public class EntityTest
    {
        [Fact]
        public void GetPropertiesSuccess()
        {
            // Arrange
            OrderProduct op = new OrderProduct()
            {
                OrderId = 1,
                ProductId = 23,
                Amount = 3,
                PricePerUnit = 4.99m
            };

            // Act
            List<KeyValuePair<string, object>> result = op.GetProperties();

            // Assert
            Assert.Equal(5, result.Count);
        }
    }
}
