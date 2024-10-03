using Moq;

namespace Test
{
    public class Unit3Mock
    {
        public interface ITaxService
        {
            decimal GetTaxRate(decimal taxRate);
        }

        // แยกการคำนวณอัตราภาษี
        public class TaxService : ITaxService
        {
            public decimal GetTaxRate(decimal taxRate)
            {
                return (1 - (taxRate / 100));
            }
        }

        public interface IService
        {
            decimal GetSomethingAfterTax(decimal r, decimal taxRate);
        }

        public class Service : IService
        {
            // ก่อนหน้าเราทำเหมือนว่ามี IServiceProvider เป็น Dependency
            // แต่ตอนนนี้เราจะเปลี่ยนไปใช้ ITaxService แทน
            private readonly ITaxService _taxService;

            public Service(ITaxService taxService)
            {
                _taxService = taxService;
            }

            public decimal GetSomethingAfterTax(decimal r, decimal taxRate)
            {
                var tax = _taxService.GetTaxRate(taxRate);
                return r * tax;
            }
        }

        public class Unit3
        {
            [Theory]
            [InlineData(10000, 15, 8500)]
            [InlineData(25000, 16, 21000)]
            public void Summary_Should_Correct(decimal total, decimal taxRate, decimal expect)
            {
                // Arrange
                // และจากนั้นจึงเปลี่ยนไป Mock ITaxService แทน
                var mock = new Mock<ITaxService>();
                mock.Setup(x => x.GetTaxRate(It.IsAny<decimal>()))
                    .Returns(1 - (taxRate / 100));

                // Act
                var handler = new Service(mock.Object);
                var result = handler.GetSomethingAfterTax(total, taxRate);

                // Assert
                mock.Verify(x => x.GetTaxRate(It.IsAny<decimal>()), Times.Once);
                Assert.Equal(expect, result);

                Assert.IsType<decimal>(result);
            }
        }
    }
}
