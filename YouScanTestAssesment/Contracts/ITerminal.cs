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
        void SetPricing(PricingStrategy strategy);
        void Scan(string Id);
        Task<double> Calculate(bool flush);
    }
}
