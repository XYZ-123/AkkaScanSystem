using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouScanTestAssesment.Messages;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment.Actors
{
    public class CalculatingActor : ReceiveActor
    {
        private int _positions;
        private ItemPricing _pricing;
        public CalculatingActor(ItemPricing pricing)
        {
            _pricing = pricing;
            Receive<ScanMessage>(message => HandleScanMessage(message));
            Receive<SetPricingMessage>(message => HandlePricingMessage(message));
            Receive<CalculateMessage>(message => HandleCalculateMessage(message));
        }
        public void HandleScanMessage(ScanMessage message)
        {
            _positions++;
        }
        public void HandlePricingMessage(SetPricingMessage message)
        {
            _pricing = message.Pricing;
        }
        public void HandleCalculateMessage(CalculateMessage message)
        {
            if (message.Flush)
                _positions = 0;

            Sender.Tell(1, Self);
        }
    }
}
