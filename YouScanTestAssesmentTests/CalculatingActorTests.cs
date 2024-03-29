﻿using Akka.TestKit;
using Akka.TestKit.Xunit2;
using Moq;
using Xunit;
using YouScanTestAssesment.Actors;
using YouScanTestAssesment.Contracts;
using YouScanTestAssesment.Messages;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesmentTests
{
    public class CalculatingActorTests : TestKit
    {
        private readonly Mock<ICalculator> calcMock;

        private readonly TestActorRef<CalculatingActorSUT> sut;

        public CalculatingActorTests()
        {
            calcMock = new Mock<ICalculator>();
            sut = ActorOfAsTestActorRef(() => new CalculatingActorSUT(calcMock.Object), "sut");
        }

        [Fact]
        public void ShouldIncrementCounter_if_ScanMessageIsReceived()
        {
            string id = "A";
            var scanMessage = new ScanMessage(id);
            sut.Tell(scanMessage);

            Assert.Equal(1, sut.UnderlyingActor.Count);
        }

        [Fact]
        public void ShouldSetPricing_if_SetPricingMessageIsReceived()
        {
            var expectedPricing = new ItemPricing(new BatchPricing(3, 2), 1);

            sut.Tell(new SetPricingMessage(expectedPricing));

            Assert.Equal(expectedPricing, sut.UnderlyingActor.Pricing);
        }

        [Fact]
        public void ShouldReturnResult_If_CalculateMessageIsReceived()
        {
            var expectedPrice = 1;

            string id = "A";
            calcMock.Setup(x => x.Calculate(It.IsAny<int>(), It.IsAny<ItemPricing>())).Returns(expectedPrice);
            var scanMessage = new ScanMessage(id);
            var pricing = new ItemPricing(new BatchPricing(3, 2), 1);

            sut.Tell(new SetPricingMessage(pricing));
            sut.Tell(scanMessage);

            sut.Tell(new CalculateMessage(true));
            var actual = ExpectMsg<double>();

            Assert.Equal(expectedPrice, actual);
        }

        [Fact]
        public void ShouldResetPositions_If_FlushFlagIsReceived()
        {
            string id = "A";
            var scanMessage = new ScanMessage(id);
            sut.Tell(scanMessage);

            sut.Tell(new CalculateMessage(true));

            Assert.Equal(0, sut.UnderlyingActor.Count);
        }

        public class CalculatingActorSUT : CalculatingActor
        {
            public CalculatingActorSUT(ICalculator calc)
                : base(calc)
            {
            }

            internal int Count
            {
                get { return _positions; }
            }

            internal ItemPricing Pricing
            {
                get { return _pricing; }
            }
        }
    }
}
