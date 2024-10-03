namespace Test
{
    public class DistanceService
    {
        public double GetKilometersDistance(double meters)
        {
            return meters.MeterToKilometers();
        }
    }

    public class Unit6MoqExtension
    {
        [Trait("OS", "Linux")]
        [Theory]
        [InlineData(6400, 6.4)]
        [InlineData(200, 0.2)]
        [InlineData(10000000000, 10000000)]
        public void GetKilometersDistance_Should_Correct(double meters, double expected)
        {
            // Arrange
            var handler = new DistanceService();

            // Act
            var result = handler.GetKilometersDistance(meters);

            // Assert
            Assert.Equal(expected, result);
        }

    }

    public static class Extension
    {
        public static double MeterToKilometers(this double meters)
        {
            return meters / 1000.0;
        }
    }
}
