using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouScanTestAssesment.Messages;

namespace YouScanTestAssesmentTests
{
    public class TestCalculatorActor : ReceiveActor
    {
        public TestCalculatorActor()
        {

            Receive<ScanMessage>(message => HandleScanMessage(message));
            Receive<SetPricingMessage>(message => HandlePricingMessage(message));
            Receive<CalculateMessage>(message => HandleCalculateMessage(message));
        }
        public void HandleScanMessage(ScanMessage message)
        {
            Sender.Tell("ReceivedScanMessage", Self);
        }
        public void HandlePricingMessage(SetPricingMessage message)
        {
            Sender.Tell("ReceivedPricingMessage", Self);
        }
        public void HandleCalculateMessage(CalculateMessage message)
        {
            Sender.Tell(10, Self);
        }
    }
}
