using System.Threading.Tasks;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment.Contracts
{
    public interface ITerminal
    {
        Task SetPricing(PricingStrategy strategy);

        Task Scan(string id);

        Task<double> Calculate(bool flush);
    }
}
