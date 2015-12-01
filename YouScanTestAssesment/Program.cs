using System;
using System.Collections.Generic;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var t = new Terminal())
            {
                double amount;
                PricingStrategy ps = new PricingStrategy
                {
                    Strategy = new Dictionary<string, ItemPricing>
                    {
                        { "A", new ItemPricing(new BatchPricing(3, 3), 1.25) },
                        { "B", new ItemPricing(4.25) },
                        { "C", new ItemPricing(new BatchPricing(6, 5), 1) },
                        { "D", new ItemPricing(0.75) }
                    }
                };

                t.SetPricing(ps);

                t.Scan("A");
                t.Scan("B");
                t.Scan("C");
                t.Scan("D");
                t.Scan("A");
                t.Scan("B");
                t.Scan("A");
                amount = t.Calculate(true).GetAwaiter().GetResult();
                Console.WriteLine("ABCDABA, price {0}", amount);

                t.Scan("C");
                t.Scan("C");
                t.Scan("C");
                t.Scan("C");
                t.Scan("C");
                t.Scan("C");
                t.Scan("C");
                amount = t.Calculate(true).GetAwaiter().GetResult();
                Console.WriteLine("CCCCCCC, price {0}", amount);

                t.Scan("A");
                t.Scan("B");
                t.Scan("C");
                t.Scan("D");
                amount = t.Calculate(true).GetAwaiter().GetResult();
                Console.WriteLine("ABCD, price {0}", amount);
                Console.ReadKey();
            }
        }
    }
}
