using Xunit;

namespace EXIFWeb.Controllers.Tests
{
    public class ReportControllerTests
    {
        public static double[] GetSamplesData()
        {
            return new double[] { 34, 34, 34, 48, 22, 22, 58, 58, 32, 18, 18, 28, 28, 26, 34, 48, 48, 2.9, 3.8, 2.9, 2.9, 2.9, 3.8, 3.8, 3.8, 3.8, 3.8, 3.8, 18, 26 };

        }

        [Theory]
        [InlineData(15.74825734250949, 29.06507599082383)]

        public void ConfidenceIntervalTest(double lowerBound, double upperBound)

        {
            double[] data = ReportControllerTests.GetSamplesData();
            (double lowerBound, double upperBound) bounds = ReportController.ConfidenceInterval(data, 0.95);

            Assert.NotNull(bounds);
            Assert.Equal(lowerBound, bounds.lowerBound);
            Assert.Equal(upperBound, bounds.upperBound);

        }

        [Fact]

        public void FrequencyTest()

        {
            double[] data = ReportControllerTests.GetSamplesData();
            var res = ReportController.Frequency(data);

            Assert.NotEmpty(res);
            Assert.Equal(10, res.Count());
            Assert.Equal(100, res.Sum(x => x.Frequency));

        }
    }
}