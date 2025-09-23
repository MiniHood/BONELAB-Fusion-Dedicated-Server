public class LuaServerAPI
{
    public void Log(string msg) => ServerCLI.AddLog("[Lua] " + msg);

    public List<FusionServer> GetServers() =>
        ServerManager.Instance.GetAllServers().ToList();

    public async Task SendCommand(FusionServer server, string command, params string[] args)
    {
        if (ServerCLI.GetRegisteredCommandNames().Contains(command))
            await server.SendMessageToClientAsync(command + " " + string.Join(' ', args));
        else
            Log($"Unknown command '{command}'");
    }
}
