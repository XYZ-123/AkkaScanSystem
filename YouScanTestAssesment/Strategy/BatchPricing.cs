using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
