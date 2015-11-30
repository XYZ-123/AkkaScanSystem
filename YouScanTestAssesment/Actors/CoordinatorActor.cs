using System;
using Akka.Actor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.DI.Core;
using YouScanTestAssesment.Messages;
using YouScanTestAssesment.Strategy;

namespace YouScanTestAssesment.Actors
{
    public class CoordinatorActor<T>: ReceiveActor where T :ReceiveActor
    {
        protected PricingStrategy _strategy = PricingStrategy.Default;
        protected readonly Dictionary<string, IActorRef> _calcActors = new Dictionary<string, IActorRef>();

        public CoordinatorActor()
        {
            Receive<SetStrategyMessage>(message => HandleSetStrategyMessage(message));
            Receive<ScanMessage>(message => HandleScanMessage(message));
            Receive<CalculateMessage>(message => HandleCalculateMessage(message));
        }

        public void HandleSetStrategyMessage(SetStrategyMessage message)
        {
            if (message.Strategy == null)
                throw new ArgumentNullException("Strategy");

            _strategy = message.Strategy;

            foreach(var actor in _calcActors.Keys)
            {
                if(_strategy.Strategy.ContainsKey(actor))
                {
                    _calcActors[actor].Forward(new SetPricingMessage(_strategy.Strategy[actor]));
                }
            }
        }

        public void HandleScanMessage(ScanMessage message)
        {
            var actor = GetOrCreateActor(message.Id);

            actor.Forward(message);
        }

        private IActorRef GetOrCreateActor(string Id)
        {
            if(_calcActors.ContainsKey(Id))
            {
                return _calcActors[Id];
            }

            var actorProps = Context.DI().Props(typeof (T));
            var actorRef = Context.ActorOf(actorProps, "CalculatingActor-"+Id);

            if (_strategy.Strategy.ContainsKey(Id))
            {
                actorRef.Forward(new SetPricingMessage(_strategy.Strategy[Id]));
            }

            _calcActors.Add(Id, actorRef);

            return actorRef;
        }
        public async Task HandleCalculateMessage(CalculateMessage message)
        {
            double amount = 0.0;
            var sender = Sender;
            var self = Self;
            var tasks = new List<Task<double>>();

            await Task.Run(async () =>
            {
                tasks.AddRange(_calcActors.Select(actor => actor.Value.Ask<double>(message)));
                var results = await Task.WhenAll(tasks);
                amount = results.Aggregate((res1, res2) => res1 + res2);
            });
                     
            sender.Tell(amount, self);
        }
    }
}
