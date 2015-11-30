using Akka.Actor;
using Moq;
using System;
using Xunit;
using Akka.TestKit;
using Akka.TestKit.Xunit;
using YouScanTestAssesment.Actors;
using YouScanTestAssesment.Contracts;
using YouScanTestAssesment.Messages;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesmentTests
{
    public class CalculatingActorTests
    {
        internal class CalculatingActorSUT : CalculatingActor
        {
            internal CalculatingActorSUT(ICalculator calc):base(calc)
            {

            }
            internal int Count { get { return _positions; } }
            internal ItemPricing Pricing { get { return _pricing; } }
        }
        
        private readonly ICalculator calcMock = Mock.Of<ICalculator>();

        private readonly IActorRef sut;
        public CalculatingActorTests()
        {
            //sut = new TestActorRef<CalculatingActorSUT>();
        }

        [Fact]
        public void TestMethod1()
        {
            string id = "A";
            var scanMessage = new ScanMessage(id);
             
        }
    }
}
