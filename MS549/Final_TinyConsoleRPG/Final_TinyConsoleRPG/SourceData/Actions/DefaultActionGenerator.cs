using System.Collections.Generic;
using SadPumpkin.Util.CombatEngine.Action;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.CostCalculators;
using SadPumpkin.Util.CombatEngine.EffectCalculators;

namespace Final_TinyConsoleRPG.SourceData.Actions
{
    public class DefaultActionGenerator : IStandardActionGenerator
    {
        public IEnumerable<IAction> GetActions(IInitiativeActor actor)
        {
            // Wait
            yield return new Action(
                0,
                "Wait",
                true,
                actor,
                new[] {(ITargetableActor) actor},
                25,
                NoCost.Instance,
                NoEffect.Instance,
                null,
                null);
        }
    }
}