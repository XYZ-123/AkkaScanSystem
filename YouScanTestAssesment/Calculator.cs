using System;
using YouScanTestAssesment.Contracts;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment
{
    public class Calculator : ICalculator
    {
        public double Calculate(int amount, ItemPricing pricing)
        {
            double batches = pricing.Batch.Quantity == 0 ? 0 : Math.Floor(Convert.ToDouble(amount / pricing.Batch.Quantity));

            double leftovers = amount - (batches * pricing.Batch.Quantity);

            return (batches * pricing.Batch.Price) + (leftovers * pricing.PerSingle);
        }
    }
}
