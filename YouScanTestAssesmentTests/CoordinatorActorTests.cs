using System.Collections.Generic;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.TestKit;
using Akka.TestKit.Xunit2;
using Autofac;
using Moq;
using Xunit;
using YouScanTestAssesment.Actors;
using YouScanTestAssesment.Contracts;
using YouScanTestAssesment.Messages;
using YouScanTestAssesment.Strategy;
using YouScanTestAssesmentTests.TestActors;

namespace YouScanTestAssesmentTests
{
    public class CoordinatorActorTests : TestKit
    {
        private readonly TestActorRef<CoordinatorActorSUT<TestCalculatorActor>> sut;
        private readonly PricingStrategy ps;
        private string testId = "A";

        public CoordinatorActorTests()
        {
            sut = ActorOfAsTestActorRef<CoordinatorActorSUT<TestCalculatorActor>>("coordactor");
            ps = new PricingStrategy
            {
                Strategy = new Dictionary<string, ItemPricing>
                    {
                        { testId, new ItemPricing(new BatchPricing(3, 3), 1.25) },
                    }
            };
            ConfigureDependencies();
        }

        [Fact]
        public void ShouldCreateChildActor_OnScanMessage_IfActorDoesntExist()
        {
            sut.Tell(new ScanMessage(testId));

            Assert.Equal(1, sut.UnderlyingActor.Actors.Count);
            Assert.NotNull(sut.UnderlyingActor.Actors[testId]);
        }

        [Fact]
        public void ShouldGetChildActor_OnScanMessage_IfActorExists()
        {
            sut.Tell(new ScanMessage(testId));
            sut.Tell(new ScanMessage(testId));

            Assert.Equal(1, sut.UnderlyingActor.Actors.Count);
            Assert.NotNull(sut.UnderlyingActor.Actors[testId]);
        }

        [Fact]
        public void ShouldSendScanMessageToChildActor_OnScanMessage()
        {
            sut.Tell(new ScanMessage(testId));

            var message = ExpectMsg<ScanMessage>();

            Assert.Equal(testId, message.Id);
        }

        [Fact]
        public void ShouldSend_SetPricingMessageToChildActor_OnScanMessage_IfActorHasPricing()
        {
            sut.Tell(new SetStrategyMessage(ps));

            sut.Tell(new ScanMessage(testId));

            var message = ExpectMsg<SetPricingMessage>();

            Assert.Equal(ps.Strategy[testId], message.Pricing);
        }

        [Fact]
        public void ShouldSetStrategy_OnSetStrategyMessage()
        {
            sut.Tell(new SetStrategyMessage(ps));

            Assert.Equal(ps, sut.UnderlyingActor.Strategy);
        }

        [Fact]
        public void ShouldSetPricing_ForChildActors_OnSetStrategyMessage()
        {
            sut.Tell(new ScanMessage(testId));

            sut.Tell(new SetStrategyMessage(ps));

            // First, we have to skip previously sent scan message
            ExpectMsg<ScanMessage>();

            var message = ExpectMsg<SetPricingMessage>();

            Assert.Equal(ps.Strategy[testId], message.Pricing);
        }

        /*Uncomment when Akka starts to work fine with async/await
        [Fact]
        public void ShouldReturnAggergatedPricing_FromChildActors()
        {
            double expected = 20;
            sut.Tell(new ScanMessage(testId));
            sut.Tell(new ScanMessage("b"));
            sut.Tell(new CalculateMessage(true));
            ExpectMsg<ScanMessage>();
            ExpectMsg<ScanMessage>();
            var msg = ExpectMsg<double>();
            Assert.Equal(20, msg);
        }*/

        private void ConfigureDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestCalculatorActor>();
            var container = builder.Build();

            var resolver = new AutoFacDependencyResolver(container, Sys);
        }

        public class CoordinatorActorSUT<T> : CoordinatorActor<T>
            where T : ReceiveActor
        {
            internal PricingStrategy Strategy => _strategy;

            internal Dictionary<string, IActorRef> Actors => _calcActors;
        }
    }
}
