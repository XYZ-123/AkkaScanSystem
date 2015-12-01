using System.Threading.Tasks;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment.Contracts
{
    public interface ITerminal
    {

        void SetPricing(PricingStrategy strategy);
        void Scan(string id);
        Task<double> Calculate(bool flush);
    }
}
