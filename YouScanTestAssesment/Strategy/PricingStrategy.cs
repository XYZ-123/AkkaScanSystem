using System.Collections.Generic;

namespace YouScanTestAssesment.Strategy
{
    public class PricingStrategy
    {
        public static PricingStrategy Default => new PricingStrategy { Strategy = new Dictionary<string, ItemPricing>() };

        public Dictionary<string, ItemPricing> Strategy { get; set; }
    }
}
