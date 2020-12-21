using System;
using System.Collections.Generic;
using SadPumpkin.Util.CombatEngine.Action;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.CharacterControllers;

namespace Final_TinyConsoleRPG.SourceData
{
    public class PlayerController : ICharacterController
    {
        public bool HasPendingSelection { get; private set; }
        public Character ActiveCharacter { get; private set; }
        public IReadOnlyDictionary<uint, IAction> Actions { get; private set; }

        private System.Action<uint> _selectAction = null;

        public void SelectAction(IInitiativeActor activeEntity, IReadOnlyDictionary<uint, IAction> availableActions, Action<uint> selectAction)
        {
            _selectAction = selectAction;

            HasPendingSelection = true;
            ActiveCharacter = (Character) activeEntity;
            Actions = availableActions;
        }

        public void MakeSelection(uint actionId)
        {
            if (!HasPendingSelection)
                return;
            if (!Actions.ContainsKey(actionId))
                return;

            _selectAction(actionId);
            _selectAction = null;

            HasPendingSelection = false;
            ActiveCharacter = null;
            Actions = null;
        }
    }
}