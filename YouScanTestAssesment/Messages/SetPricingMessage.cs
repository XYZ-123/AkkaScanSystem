using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment.Messages
{
    public class SetPricingMessage
    {
        public SetPricingMessage(ItemPricing pricing)
        {
            Pricing = pricing;
        }

        public ItemPricing Pricing { get; private set; }
    }
}
