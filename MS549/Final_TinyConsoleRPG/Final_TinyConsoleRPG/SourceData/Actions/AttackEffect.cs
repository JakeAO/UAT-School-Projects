using System;
using System.Collections.Generic;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.EffectCalculators;

namespace Final_TinyConsoleRPG.SourceData.Actions
{
    public class AttackEffect : IEffectCalc
    {
        private static readonly Random RANDOM = new Random();

        public string Description => "Deals [5-15] Damage";

        public void Apply(IInitiativeActor sourceEntity, IReadOnlyCollection<ITargetableActor> targetActors)
        {
            int damage = 5 + RANDOM.Next(10);
            foreach (var targetActor in targetActors)
            {
                if (targetActor is Character character)
                {
                    character.CurrentHealth = (uint) Math.Max(0u, character.CurrentHealth - damage);
                }
            }
        }
    }
}