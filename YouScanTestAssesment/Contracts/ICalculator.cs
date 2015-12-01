using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment.Contracts
{
    public interface ICalculator
    {
        double Calculate(int amount, ItemPricing pricing);
    }
}
