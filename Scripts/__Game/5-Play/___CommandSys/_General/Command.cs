namespace CommandSystem
{
    public interface ICommandInfo { }
    public interface ICommandResult { }

    public struct Command
    {

        public CommandType Type;
        public ICommandInfo Info;
        public ICommandResult Result;

        public Command(CommandType type, ICommandInfo info)
        {
            Type = type;
            Info = info;
            Result = null;
        }
    }
}
