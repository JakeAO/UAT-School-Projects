using System.Collections.Generic;
using SadPumpkin.Util.CombatEngine;
using SadPumpkin.Util.CombatEngine.Action;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.CostCalculators;
using SadPumpkin.Util.CombatEngine.EffectCalculators;

namespace Final_TinyConsoleRPG.SourceData.Actions
{
    public class Action : IAction
    {
        public uint Id { get; }
        public string Name { get; }
        public bool Available { get; }
        public IInitiativeActor Source { get; }
        public IReadOnlyCollection<ITargetableActor> Targets { get; }
        public uint Speed { get; }
        public ICostCalc Cost { get; }
        public IEffectCalc Effect { get; }
        public IIdTracked ActionSource { get; }
        public IIdTracked ActionProvider { get; }

        public Action(
            uint id,
            string name,
            bool available,
            IInitiativeActor source,
            IReadOnlyCollection<ITargetableActor> targets,
            uint speed,
            ICostCalc cost,
            IEffectCalc effect,
            IIdTracked actionSource,
            IIdTracked actionProvider)
        {
            Id = id;
            Name = name;
            Available = available;
            Source = source;
            Targets = targets;
            Speed = speed;
            Cost = cost;
            Effect = effect;
            ActionSource = actionSource;
            ActionProvider = actionProvider;
        }
    }
}