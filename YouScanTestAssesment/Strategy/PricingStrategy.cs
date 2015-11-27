using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouScanTestAssesment.Strategy
{
    public class PricingStrategy
    {
        public Dictionary<string, ItemPricing> Strategy { get; set; }

        public static PricingStrategy Default => new PricingStrategy {Strategy = new Dictionary<string, ItemPricing>()};

    }
}
