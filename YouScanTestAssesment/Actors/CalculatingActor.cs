using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YouScanTestAssesment.Contracts;
using YouScanTestAssesment.Messages;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment.Actors
{
    public class CalculatingActor : ReceiveActor
    {
        protected int _positions;
        protected ItemPricing _pricing;
        protected readonly ICalculator _calculator;

        public CalculatingActor(ICalculator calculator)
        {
            if (calculator == null)
                throw new ArgumentNullException("calculator");

            _calculator = calculator;
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
            var amount = _calculator.Calculate(_positions, _pricing);

            if (message.Flush)
                _positions = 0;

            Sender.Tell(amount, Self);
        }
    }
}
