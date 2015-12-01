using System;
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
    public class Terminal : ITerminal, IDisposable
    {
        private readonly ActorSystem system;
        private readonly IActorRef coordinatorActor;

        public Terminal()
        {
            system = ActorSystem.Create("Terminal");
            ConfigureDependecies();
            coordinatorActor = system.ActorOf<CoordinatorActor<CalculatingActor>>();
        }

        public async Task<double> Calculate(bool flush)
        {
           var result = await coordinatorActor.Ask<double>(new CalculateMessage(flush), TimeSpan.FromSeconds(2)).ConfigureAwait(false);
           return result;
        }

        public void Scan(string id)
        {
            coordinatorActor.Tell(new ScanMessage(id));
        }

        public void SetPricing(PricingStrategy strategy)
        {
            if (strategy == null)
            {
                throw new ArgumentNullException("strategy");
            }

            coordinatorActor.Tell(new SetStrategyMessage(strategy));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                system.Dispose();
            }
        }

        private void ConfigureDependecies()
        {
            var builder = new Autofac.ContainerBuilder();
            builder.RegisterType<Calculator>().As<ICalculator>().SingleInstance();
            builder.RegisterType<CalculatingActor>();
            var container = builder.Build();

            var resolver = new AutoFacDependencyResolver(container, system);
        }
    }
}
