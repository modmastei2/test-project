using Moq;

namespace Test
{
    public class Unit5ThrowMoq
    {
        public interface ICalculator
        {
            decimal Divide(decimal value, decimal divider);
        }
        // ฟังก์ชั่นที่จะโยน DivideByZeroException กรณีที่ตีวหารมีค่าเป็น 0
        public class Calculator : ICalculator
        {
            public decimal Divide(decimal value, decimal divider)
            {
                if(divider == decimal.Zero)
                    throw new DivideByZeroException("Cannot divide by zero.");

                return value / divider;
            }
        }

        public class Unit5
        {
            [Fact]
            [Trait("Risk", "High")]
            [Trait("Risk", "Low")]
            public void Divide_Should_Fail_IfDividerIsZero()
            {
                // Arrange
                var mock = new Mock<ICalculator>();
                mock.Setup(service => service.Divide(It.IsAny<decimal>(), 0))
                    .Throws<DivideByZeroException>();


                // Act & Assert
                Assert.Throws<DivideByZeroException>(() => mock.Object.Divide(5, 0));
            }
        }
    }
}
