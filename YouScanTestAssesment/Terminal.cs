using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.AutoFac;
using Autofac;
using YouScanTestAssesment.Actors;
using YouScanTestAssesment.Contracts;
using YouScanTestAssesment.Messages;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment
{
    public class Terminal : ITerminal
    {
        private readonly ActorSystem system;
        private readonly IActorRef coordinatorActor;

        public Terminal()
        {
            system = ActorSystem.Create("Terminal");
            ConfigureDependecies();
            coordinatorActor = system.ActorOf<CoordinatorActor>();
        }

        public async Task<double> Calculate(bool flush)
        {
           var result = await coordinatorActor.Ask<double>(new CalculateMessage(flush));
           return result;
        }

        public async Task Scan(string Id)
        {
            coordinatorActor.Tell(new ScanMessage(Id));
        }

        public async Task SetPricing(PricingStrategy strategy)
        {
            coordinatorActor.Tell(new SetStrategyMessage(strategy));
        }

        private void ConfigureDependecies()
        {
            var builder = new Autofac.ContainerBuilder();
            builder.RegisterType<Calculator>().As<ICalculator>().SingleInstance();
            var container = builder.Build();

            var resolver = new AutoFacDependencyResolver(container, system);
        }
    }
}
