using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment.Contracts
{
    public interface ICalculator
    {
        double Calculate(int amount, ItemPricing pricing);
    }
}
