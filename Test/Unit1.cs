using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Unit1HowTo
    {
        public interface IService
        {
            decimal GetSomethingAfterTax(decimal r, decimal taxRate);
        }

        public class Service : IService
        {
            private readonly IServiceProvider _serviceProvider;

            public Service(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            public decimal GetSomethingAfterTax(decimal r, decimal taxRate)
            {
                return r * (1 - (taxRate / 100));
            }
        }

        public class UnitTest1
        {
            [Theory]
            [InlineData(10000, 15, 8500)]
            [InlineData(25000, 16, 21000)]
            public void GetSomethingAfterTax_Should_Correct(decimal total, decimal taxRate, decimal expect)
            {
                // Arrange
                var mock = new Mock<IServiceProvider>();

                // Act
                var handler = new Service(mock.Object);
                var result = handler.GetSomethingAfterTax(total, taxRate);

                // Assert
                Assert.Equal(expect, result);
            }
        }
    }
}
