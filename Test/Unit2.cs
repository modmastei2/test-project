using Moq;

namespace Test
{
    public class Unit2Stub
    {
        public interface ITaxService
        {
            decimal GetTaxRate(decimal taxRate);
        }

        // �¡��äӹǳ�ѵ������
        public class TaxService
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
            // ��͹˹����ҷ�����͹����� IServiceProvider �� Dependency
            // ��͹������Ҩ�����¹��� ITaxServcie ᷹
            private readonly ITaxService _taxService;

            public Service(ITaxService taxService)
            {
                _taxService = taxService;
            }

            public decimal GetSomethingAfterTax(decimal r, decimal taxRate)
            {
                return r * _taxService.GetTaxRate(taxRate);
            }
        }

        public class Unit2
        {
            [Theory]
            [InlineData(10000, 15, 8500)]
            [InlineData(25000, 16, 21000)]
            public void Summary_Should_Correct(decimal total, decimal taxRate, decimal expect)
            {
                // Arrange
                // ��Шҡ��鹨֧����¹� Mock ITaxService ᷹
                var mock = new Mock<ITaxService>();
                mock.Setup(x => x.GetTaxRate(It.IsAny<decimal>()))
                    .Returns(1 - (taxRate / 100));

                // Act
                var handler = new Service(mock.Object);
                var result = handler.GetSomethingAfterTax(total, taxRate);

                // Assert
                Assert.Equal(expect, result);
            }
        }
    }
}
