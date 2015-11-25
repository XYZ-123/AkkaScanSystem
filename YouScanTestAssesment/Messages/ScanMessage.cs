using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouScanTestAssesment.Messages
{
    public class ScanMessage
    {
        public ScanMessage(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }

}
