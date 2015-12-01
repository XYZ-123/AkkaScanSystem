namespace YouScanTestAssesment.Strategy
{
    public struct BatchPricing
    {
        public BatchPricing(int quantity, double price)
        {
            Quantity = quantity;
            Price = price;
        }

        public int Quantity { get; private set; }

        public double Price { get; private set; }
    }
}
