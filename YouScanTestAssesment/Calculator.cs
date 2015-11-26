using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouScanTestAssesment.Contracts;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment
{
    public class Calculator : ICalculator
    {
        public double Calculate(int amount, ItemPricing pricing)
        {
            double batches = Math.Floor(Convert.ToDouble(amount / pricing.Batch.Quantity));

            double leftovers = amount - batches * pricing.Batch.Quantity;

            return batches * pricing.Batch.Price + leftovers * pricing.PerSingle;
        }
    }
}
