using Xunit;
using YouScanTestAssesment;
using YouScanTestAssesment.Contracts;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesmentTests
{
    public class CalculatorTests
    {
        private readonly ICalculator sut = new Calculator();

        [Fact]
        public void ShouldReturnCorrectPrice()
        {
            var expected = 8;
            ItemPricing pricing = new ItemPricing(new BatchPricing(3, 4), 2);
            var actual = sut.Calculate(5, pricing);
            Assert.Equal(expected, actual);
        }
    }
}
