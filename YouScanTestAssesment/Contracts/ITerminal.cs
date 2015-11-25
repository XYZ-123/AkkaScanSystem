using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment.Contracts
{
    interface ITerminal
    {
        Task SetPricing(PricingStrategy strategy);
        Task Scan(string Id);
        Task<double> Calculate(bool flush);
    }
}
