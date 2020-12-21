using System.Collections.Generic;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.CharacterControllers;
using SadPumpkin.Util.CombatEngine.Party;

namespace Final_TinyConsoleRPG.SourceData
{
    public class Party : IParty
    {
        public uint Id { get; }
        public ICharacterController Controller { get; }
        public IReadOnlyCollection<IInitiativeActor> Actors { get; }

        public Party(
            uint id,
            ICharacterController controller,
            IReadOnlyCollection<IInitiativeActor> actors)
        {
            Id = id;
            Controller = controller;
            Actors = actors;
        }
    }
}