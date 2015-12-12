using Akka.Actor;
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
            Context.System.EventStream.Publish(message);
            Sender.Tell(message, Self);
        }

        public void HandlePricingMessage(SetPricingMessage message)
        {
            Context.System.EventStream.Publish(message);
            Sender.Tell(message, Self);
        }

        public void HandleCalculateMessage(CalculateMessage message)
        {
            Context.System.EventStream.Publish(10);
            Sender.Tell(10, Self);
        }
    }
}
