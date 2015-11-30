using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.TestKit;
using Akka.TestKit.Xunit2;
using Autofac;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using YouScanTestAssesment;
using YouScanTestAssesment.Actors;
using YouScanTestAssesment.Contracts;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesmentTests
{
    public class CoordinatorActorTests: TestKit
    {
        public class CoordinatorActorSUT<T>: CoordinatorActor<T>
        {
            internal PricingStrategy Strategy { get { return _strategy; } }
            internal Dictionary<string, IActorRef> actors { get { return _calcActors; } }
        }

        private readonly TestActorRef<CoordinatorActorSUT<> sut;
        private readonly Mock<ICalculator> calcMock;
        public CoordinatorActorTests()
        {
            calcMock = new Mock<ICalculator>();
            calcMock.Setup(x => x.Calculate(It.IsAny<int>(), It.IsAny<ItemPricing>())).Returns(1);

            sut = ActorOfAsTestActorRef<CoordinatorActorSUT>("coordactor");

            ConfigureDependencies();
        }

        private void ConfigureDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance<ICalculator>(calcMock.Object);
            
            builder.RegisterType<CalculatingActor>();
            var container = builder.Build();

            var resolver = new AutoFacDependencyResolver(container, Sys);
        }

        [Fact]
        public void ShouldCreateChildActor_OnScanMessage_IfActorDoesntExist()
        {

        }

        [Fact]
        public void ShouldSetStrategy_OnSetStrategyMessage()
        {

        }
    }
}
