public class ServerCommand
{
    public string Name { get; }
    public Func<FusionServer, List<string>, Task> Action { get; }

    public ServerCommand(string name, Func<FusionServer, List<string>, Task> action)
    {
        Name = name;
        Action = action;
    }
}
