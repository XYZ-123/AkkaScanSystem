using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouScanTestAssesment.Messages
{
    public class AmountMessage
    {
        public AmountMessage(int amount)
        {
            Amount = amount;
        }
        public int Amount { get; private set; }
    }
}
