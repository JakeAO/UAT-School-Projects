using System.Collections.Generic;
using Final_TinyConsoleRPG.SourceData.Actions;
using SadPumpkin.Util.CombatEngine.Action;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.CostCalculators;
using SadPumpkin.Util.CombatEngine.TargetCalculators;
using Action = Final_TinyConsoleRPG.SourceData.Actions.Action;

namespace Final_TinyConsoleRPG.SourceData
{
    public class Character : ITargetableActor
    {
        public uint Id { get; }
        public uint Party { get; }
        public string Name { get; }
        public uint CurrentHealth { get; set; }
        public uint MaxHealth { get; }

        public bool IsAlive() => CurrentHealth > 0u;
        public bool CanTarget() => IsAlive();
        public float GetInitiative() => 10f;

        public Character(
            uint id,
            uint partyId,
            string name,
            uint health)
        {
            Id = id;
            Party = partyId;
            Name = name;
            CurrentHealth = MaxHealth = health;
        }

        public IReadOnlyCollection<IAction> GetAllActions(IReadOnlyCollection<ITargetableActor> possibleTargets)
        {
            List<IAction> allActions = new List<IAction>(10);


            uint abilityId = 100;

            // Attack
            var targetGroups = SingleEnemyTargetCalculator.Instance.GetTargetOptions(this, possibleTargets);
            foreach (var targetGroup in targetGroups)
            {
                allActions.Add(new Action(
                    abilityId++,
                    "Attack",
                    true,
                    this,
                    targetGroup,
                    100,
                    NoCost.Instance,
                    new AttackEffect(),
                    null,
                    null));
            }

            return allActions;
        }

        public IInitiativeActor Copy()
        {
            return new Character(Id, Party, Name, MaxHealth)
            {
                CurrentHealth = CurrentHealth
            };
        }
    }
}