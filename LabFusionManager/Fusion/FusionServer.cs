using System.IO.Pipes;
using System.Text;

public class FusionServer
{
    public string ClientId { get; private set; }
    public string DisplayName { get; private set; }
    public string ClientPipeName { get; private set; }

    private bool _isConnected = true;

    public FusionServer(string clientId, string displayName, string clientPipeName)
    {
        ClientId = clientId;
        DisplayName = displayName;
        ClientPipeName = clientPipeName;

        ServerCLI.RegisterServerCommand("echo", async (client, args) =>
        {
            await client.SendMessageToClientAsync("echo " + string.Join(' ', args));
        });

        ServerCLI.RegisterServerCommand("getPlayers", async (client, args) =>
        {
            var players = await client.RequestPlayersAsync();
            ServerCLI.AddLog($"Players on {client.DisplayName}: {string.Join(", ", players)}");
        });


    }

    public async Task<List<string>> RequestPlayersAsync()
    {
        using var pipeClient = new NamedPipeClientStream(".", ClientPipeName, PipeDirection.InOut);
        await pipeClient.ConnectAsync();

        byte[] buffer = Encoding.UTF8.GetBytes("getPlayers");
        await pipeClient.WriteAsync(buffer, 0, buffer.Length);

        byte[] responseBuffer = new byte[4096];
        int bytesRead = await pipeClient.ReadAsync(responseBuffer, 0, responseBuffer.Length);
        string json = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);

        var players = System.Text.Json.JsonSerializer.Deserialize<List<string>>(json);
        return players;
    }

    public async Task<bool> PingAsync()
    {
        try
        {
            using var clientPipe = new NamedPipeClientStream(".", ClientPipeName, PipeDirection.InOut);
            var connectTask = clientPipe.ConnectAsync();
            if (await Task.WhenAny(connectTask, Task.Delay(500)) != connectTask)
            {
                _isConnected = false;
                return false;
            }

            byte[] pingMessage = Encoding.UTF8.GetBytes("ping");
            await clientPipe.WriteAsync(pingMessage, 0, pingMessage.Length);

            var buffer = new byte[256];
            int bytesRead = await clientPipe.ReadAsync(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            return response.Trim() == "pong";
        }
        catch
        {
            _isConnected = false;
            return false;
        }
    }

    public bool IsConnected() => _isConnected;

    public async Task SendMessageToClientAsync(string message)
    {
        using var clientPipe = new NamedPipeClientStream(".", ClientPipeName, PipeDirection.InOut);
        await clientPipe.ConnectAsync();

        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await clientPipe.WriteAsync(buffer, 0, buffer.Length);

        byte[] responseBuffer = new byte[256];
        int bytesRead = await clientPipe.ReadAsync(responseBuffer, 0, responseBuffer.Length);
        string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
        Console.WriteLine($"[Server] Client {DisplayName} responded: {response}");
    }
}
