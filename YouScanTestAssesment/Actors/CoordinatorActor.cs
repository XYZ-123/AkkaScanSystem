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
    public class CoordinatorActor: ReceiveActor
    {
        private PricingStrategy _strategy;
        private Dictionary<string, IActorRef> _calcActors = new Dictionary<string, IActorRef>();
        public CoordinatorActor()
        {
            Receive<SetStrategyMessage>(message => HandleSetStrategyMessage(message));
            Receive<ScanMessage>(message => HandleScanMessage(message));
            Receive<CalculateMessage>(message => HandleCalculateMessage(message));
        }

        public void HandleSetStrategyMessage(SetStrategyMessage message)
        {
            _strategy = message.Strategy;

            foreach(var actor in _calcActors.Keys)
            {
                if(_strategy.Strategy.ContainsKey(actor))
                {
                    _calcActors[actor].Tell(new SetPricingMessage(_strategy.Strategy[actor]));
                }
            }
        }

        public void HandleScanMessage(ScanMessage message)
        {
            var actor = GetOrCreateActor(message.Id);

            actor.Tell(message);
        }

        private IActorRef GetOrCreateActor(string Id)
        {
            if(_calcActors.ContainsKey(Id))
            {
                return _calcActors[Id];
            }
            
            var actorRef = Context.ActorOf(Props.Create<CalculatingActor>(() => new CalculatingActor(_strategy.Strategy[Id])),"CalculatingActor-"+Id);
            _calcActors.Add(Id, actorRef);

            return actorRef;
        }
        public async Task<double> HandleCalculateMessage(CalculateMessage message)
        {
            double amount = 0;
            var tasks = new List<Task<double>>();

            await Task.Run(async () =>
            {
                foreach(var actor in _calcActors)
                {
                    var t = actor.Value.Ask<double>(message);
                    tasks.Add(t);
                }
                var results = await Task.WhenAll(tasks);
                amount = results.Aggregate((res1, res2) => res1 + res2);
            });
                     
            return amount;
        }
    }
}
