namespace YouScanTestAssesment.Strategy
{
    public struct ItemPricing
    {
        public ItemPricing(double perSingle)
            : this(new BatchPricing(), perSingle)
        {
        }

        public ItemPricing(BatchPricing batch, double perSingle)
        {
            PerSingle = perSingle;
            Batch = batch;
        }

        public BatchPricing Batch { get; private set; }

        public double PerSingle { get; private set; }
    }
}
