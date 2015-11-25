using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouScanTestAssesment.Contracts;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment
{
    public class Terminal : ITerminal
    {
        public Terminal()
        {

        }

        public async Task<double> Calculate(bool flush)
        {
            throw new NotImplementedException();
        }

        public async Task Scan(string Id)
        {
            throw new NotImplementedException();
        }

        public Task SetPricing(PricingStrategy strategy)
        {
            throw new NotImplementedException();
        }
    }
}
