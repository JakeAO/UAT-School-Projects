using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Final_TinyConsoleRPG.SourceData;
using Final_TinyConsoleRPG.SourceData.Actions;
using SadPumpkin.Util.CombatEngine;
using SadPumpkin.Util.CombatEngine.CharacterControllers;
using SadPumpkin.Util.CombatEngine.GameState;
using SadPumpkin.Util.CombatEngine.Party;
using SadPumpkin.Util.CombatEngine.Signals;
using Spectre.Console;

using Action = Final_TinyConsoleRPG.SourceData.Actions.Action;

namespace Final_TinyConsoleRPG
{
    public class Gameplay
    {
        private readonly IParty _playerParty = null;
        private readonly IParty _opponentParty = null;
        private readonly PlayerController _playerController = null;
        private readonly CombatManager _combatManager = null;

        private readonly GameStateUpdated _gameStateUpdated = new GameStateUpdated();
        private readonly CombatComplete _combatComplete = new CombatComplete();

        private Action<int> _onResult = null;

        private readonly ConcurrentQueue<IGameState> _pendingGameStates = new ConcurrentQueue<IGameState>();

        public Gameplay()
        {
            uint localPartyId = (uint) Guid.NewGuid().GetHashCode();
            uint opponentPartyId = (uint) Guid.NewGuid().GetHashCode();

            _playerController = new PlayerController();

            _playerParty = new Party(
                localPartyId,
                _playerController,
                new[]
                {
                    new Character(
                        (uint) Guid.NewGuid().GetHashCode(),
                        localPartyId,
                        "Jessie",
                        100),
                    new Character(
                        (uint) Guid.NewGuid().GetHashCode(),
                        localPartyId,
                        "James",
                        100),
                });
            _opponentParty = new Party(
                opponentPartyId,
                new RandomCharacterController(),
                new[]
                {
                    new Character(
                        (uint) Guid.NewGuid().GetHashCode(),
                        opponentPartyId,
                        "Inky",
                        60),
                    new Character(
                        (uint) Guid.NewGuid().GetHashCode(),
                        opponentPartyId,
                        "Blinky",
                        60),
                    new Character(
                        (uint) Guid.NewGuid().GetHashCode(),
                        opponentPartyId,
                        "Pinky",
                        60),
                });

            _combatManager = new CombatManager(
                new[] {_playerParty, _opponentParty},
                new DefaultActionGenerator(),
                100f,
                _gameStateUpdated,
                _combatComplete);

            _gameStateUpdated.Listen(newState =>
            {
                if (_pendingGameStates.All(x => x.Id != newState.Id))
                {
                    _pendingGameStates.Enqueue(newState);
                }
            });
        }

        public void Run(Action<int> onResult)
        {
            _onResult = onResult;
            Task.Run(StartCombatManager);
            Task.Run(StartRenderingConsole);
        }

        private void StartCombatManager()
        {
            _combatManager.Start(false);
        }

        private void StartRenderingConsole()
        {
            while (true)
            {
                var console = AnsiConsole.Console;

                if (_pendingGameStates.TryPeek(out IGameState gameState))
                {
                    console.Clear(true);
                    switch (gameState.State)
                    {
                        case CombatState.Completed:
                            RenderEndGame(console, _combatManager.WinningPartyId.Value);
                            break;
                        default:
                            RenderGameState(console, gameState);

                            if (_pendingGameStates.Count > 1)
                                _pendingGameStates.TryDequeue(out _);

                            if (_pendingGameStates.Count == 1 &&
                                _playerController.HasPendingSelection)
                            {
                                RenderController(console, _playerController);
                            }

                            break;
                    }
                }

                Thread.Sleep(1000);
            }
        }

        private void RenderEndGame(IAnsiConsole console, uint winningParty)
        {
            console.WriteLine("Combat Complete!");
            if (winningParty.Equals(_playerParty.Id))
            {
                console.WriteLine("You have won!", Style.Parse("green"));
            }
            else
            {
                console.WriteLine("You have lost.", Style.Parse("red"));
            }

            console.WriteLine("Press any key to exit.");
            console.Input.ReadKey(true);
            _onResult?.Invoke(0);
        }

        private void RenderGameState(IAnsiConsole console, IGameState gameState)
        {
            console.WriteLine($"GameState Id: {gameState.Id}");
            console.WriteLine($"GameState Mode: {gameState.State}");
            console.WriteLine($"Turn Order: {string.Join(", ", gameState.InitiativePreview.Select(x => x.Name))}");
            console.Render(new Rule("[red bold]Enemy Party[/]")
            {
                Alignment = Justify.Left,
                Border = BoxBorder.Heavy,
                Style = Style.Parse("red dim"),
            });
            console.Render(CreatePartyTable(_opponentParty));
            console.Render(new Rule("[green bold]Ally Party[/]")
            {
                Alignment = Justify.Left,
                Border = BoxBorder.Heavy,
                Style = Style.Parse("green dim"),
            });
            console.Render(CreatePartyTable(_playerParty));
            console.Render(new Rule("[grey bold]Controller[/]")
            {
                Alignment = Justify.Left,
                Border = BoxBorder.Heavy,
                Style = Style.Parse("grey dim"),
            });
        }

        private Table CreatePartyTable(IParty party)
        {
            Character[] partyChars = party.Actors.Select(x => x as Character).ToArray();

            var table = new Table();
            foreach (var actor in partyChars)
            {
                table.AddColumn(actor.IsAlive()
                    ? $"{actor.Name}"
                    : $"[red]{actor.Name}[/]");
            }

            table.AddRow(partyChars.Select(x => $"HP: {x.CurrentHealth}/{x.MaxHealth}").ToArray());

            return table;
        }

        private void RenderController(IAnsiConsole console, PlayerController controller)
        {
            if (!controller.HasPendingSelection)
            {
                console.WriteLine("No pending actions...", Style.Parse("grey dim"));
                return;
            }

            console.WriteLine($"Selecting actions for {controller.ActiveCharacter.Name}...");

            foreach (var actionKvp in controller.Actions)
            {
                Action asAction = actionKvp.Value as Action;
                console.WriteLine($"[{asAction.Id}] {asAction.Name} (\"{asAction.Effect.Description}\")" +
                                  $"\n   Target: {string.Join(" & ", asAction.Targets.Select(x => x.Name))}");
            }

            TextPrompt<uint> actionPrompt = new TextPrompt<uint>("Please select an Action by Id")
                .ShowChoices(false)
                .InvalidChoiceMessage("[red]Invalid action selected.[/]");
            foreach (var actionKvp in controller.Actions)
            {
                actionPrompt.AddChoice(actionKvp.Key);
            }

            uint actionId = console.Prompt(actionPrompt);
            controller.MakeSelection(actionId);
        }
    }
}