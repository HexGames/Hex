using System;
using System.Collections.Generic;

namespace CommandSystem
{
    public static class CommandSys
    {
        private struct CommandHandlers
        {
            public Func<ICommandInfo, ICommandResult> Handler;
            public List<Action<ICommandResult>> ResultHandlers;

            public CommandHandlers(Func<ICommandInfo, ICommandResult> handler)
            {
                Handler = handler;
                ResultHandlers = new List<Action<ICommandResult>>();
            }
        }

        private static readonly Dictionary<Phase, Dictionary<CommandType, CommandHandlers>> _perPhaseCommandHandlers = new Dictionary<Phase, Dictionary<CommandType, CommandHandlers>>();
        private static readonly Queue<Command> _commands = new Queue<Command>();

        public static Phase CurrentPhase;

        public static void RegisterCommandHandler(Phase phase, CommandType type, Func<ICommandInfo, ICommandResult> handler)
        {
            if (_perPhaseCommandHandlers.TryGetValue(phase, out Dictionary<CommandType, CommandHandlers> phaseCommandHandlers) == true)
            {
                if (phaseCommandHandlers.TryGetValue(type, out CommandHandlers commandHandlers) == true)
                {
                    Debug.LogError($"Handler for command type {type} in phase {phase} is already registered. Only one handler per command type in a specific phase.");
                    return;
                }
            }
            else
            {
                phaseCommandHandlers = new Dictionary<CommandType, CommandHandlers>();
                _perPhaseCommandHandlers[phase] = phaseCommandHandlers;
            }

            phaseCommandHandlers[type] = new CommandHandlers(handler);
        }

        public static void RegisterResultHandler(Phase phase, CommandType type, Action<ICommandResult> handler)
        {
            if (_perPhaseCommandHandlers.TryGetValue(phase, out Dictionary<CommandType, CommandHandlers> phaseCommandHandlers) == true)
            {
                if (phaseCommandHandlers.TryGetValue(type, out CommandHandlers commandHandlers) == true)
                {
                    commandHandlers.ResultHandlers.Add(handler);
                }
                else
                {
                    Debug.LogError($"No command handler for command type {type} in phase {phase} found. Register a command handler before registering result handlers.");
                    return;
                }
            }
            else
            {
                Debug.LogError($"No command handlers for phase {phase} found. Register a command handler before registering result handlers.");
                return;
            }
        }

        public static void AddCommand(Command command)
        {
            _commands.Enqueue(command);
        }

        public static void ProcessAllCommands()
        {
            while (_commands.Count > 0)
            {
                Command command = _commands.Dequeue();
                if (_perPhaseCommandHandlers.TryGetValue(CurrentPhase, out Dictionary<CommandType, CommandHandlers> phaseCommandHandlers) == true)
                {
                    if (phaseCommandHandlers.TryGetValue(command.Type, out CommandHandlers commandHandlers) == true)
                    {
                        command.Result = commandHandlers.Handler(command.Info);
                        foreach (var resultHandler in commandHandlers.ResultHandlers)
                        {
                            resultHandler(command.Result);
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"No command handler for command type {command.Type} in phase {CurrentPhase} found.");
                    }
                }
                else
                {
                    Debug.LogWarning($"No command handlers for phase {CurrentPhase} found.");
                }
            }
        }

        public static void SetPhase(Phase phase)
        {
            CurrentPhase = phase;
        }
    }
}
