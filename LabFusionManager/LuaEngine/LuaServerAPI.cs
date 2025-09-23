public class LuaServerAPI
{
    public void Log(string msg) => ServerCLI.AddLog("[Lua] " + msg);

    public List<FusionServer> GetServers() =>
        ServerManager.Instance.GetAllServers().ToList();

    public bool Trim(FusionServer server)
    {
        bool result = server.TrimMemory();
        Log(result
            ? $"Trimmed memory of {server.DisplayName}"
            : $"Failed to trim memory of {server.DisplayName}");
        return result;
    }

    public async Task SendCommand(FusionServer server, string command, params string[] args)
    {
        await server.SendMessageToClientAsync(command + " " + string.Join(' ', args));
    }
}
