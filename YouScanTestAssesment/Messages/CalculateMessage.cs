using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouScanTestAssesment.Messages
{
    public class CalculateMessage
    {
        public CalculateMessage(bool flush)
        {
            Flush = flush;
        }
         
        public bool Flush { get; private set; }
    }
}
