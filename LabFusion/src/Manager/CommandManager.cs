public static class CommandManager
{
    private static readonly Dictionary<string, IFusionCommand> _commands = new(StringComparer.OrdinalIgnoreCase);

    public static void Init(FusionClient client)
    {
        Register(new GetPlayersCommand());
        Register(new ChangeSettingCommand());
    }

    public static void Register(IFusionCommand command)
    {
        _commands[command.Name] = command;
    }

    public static bool TryExecute(string name, List<string> args, out Task<object> result)
    {
        if (_commands.TryGetValue(name, out var command))
        {
            result = command.ExecuteAsync(args);
            return true;
        }

        result = null;
        return false;
    }
}
