using Moq;

namespace Test
{
    public class Unit4Exception
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

        public class Unit4
        {
            [Fact]
            public void Divide_Should_ThrowDivideByZeroException_IfDividerIsZero()
            {
                // Arrange
                var handler = new Calculator();

                // Act & Assert
                Assert.Throws<DivideByZeroException>(() => handler.Divide(5, 0));
            }

            [Fact]
            public void Divide_Should_Fail_IfDividerIsZero()
            {
                // Arrange
                var handler = new Calculator();

                // Act & Assert
                var result = Assert.Throws<DivideByZeroException>(() => handler.Divide(5, 0));

                Assert.Equal("Cannot divide by zero.", result.Message);
            }

            public void LoopInt(int initialLoop)
            {
                for (int i = initialLoop; i < (initialLoop + 10); i++)
                {
                    // implement something
                }
            }
        }
    }
}
