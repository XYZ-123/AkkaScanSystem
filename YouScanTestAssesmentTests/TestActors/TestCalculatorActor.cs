using Akka.Actor;
using Akka.Event;
using YouScanTestAssesment.Messages;

namespace YouScanTestAssesmentTests.TestActors
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
            Sender.Tell(message, Self);
        }
        public void HandlePricingMessage(SetPricingMessage message)
        {
            Sender.Tell(message, Self);
        }

        public void HandleCalculateMessage(CalculateMessage message)
        {
            Sender.Tell(10, Self);
        }
    }
}
