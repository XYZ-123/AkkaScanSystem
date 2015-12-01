using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment.Messages
{
    public class SetStrategyMessage
    {
        public SetStrategyMessage(PricingStrategy strategy)
        {
            Strategy = strategy;
        }

        public PricingStrategy Strategy { get; private set; }
    }
}
