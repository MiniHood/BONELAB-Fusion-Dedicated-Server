using LabFusion.Network;
using LabFusion.Utilities;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using UnityEngine;

public class FusionClient
{
    public string DisplayName { get; private set; }
    public string UniqueId { get; private set; }

    private string _clientPipeName;
    private string _serverPipeName;
    private Dictionary<string, Func<List<string>, Task<object>>> _asyncCommands = new();
    
    public FusionClient(string displayName, string uniqueId)
    {
        DisplayName = displayName;
        UniqueId = uniqueId;
        _clientPipeName = $"LabFusionClientPipe_{uniqueId}";
        _serverPipeName = "LabFusionServerPipe";

        CommandManager.Init(this);
    }

    public async Task EnsureRegisteredAsync(int retryDelayMs = 2000)
    {
        while (true)
        {
            try
            {
        using var pipe = new NamedPipeClientStream(".", "LabFusionRegistrationPipe", PipeDirection.InOut);
                await pipe.ConnectAsync(1000);

        int procID = Process.GetCurrentProcess().Id;
        string message = $"{DisplayName}:{UniqueId}:{_clientPipeName}:{procID}";
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await pipe.WriteAsync(buffer, 0, buffer.Length);

        byte[] responseBuffer = new byte[256];
        int bytesRead = await pipe.ReadAsync(responseBuffer, 0, responseBuffer.Length);
        string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
        FusionLogger.Log($"[Client {DisplayName}] Registration response: {response}");

                break; 
            }
            catch
            {
                FusionLogger.Log($"[Client {DisplayName}] Registration failed. Retrying in {retryDelayMs}ms...");
                await Task.Delay(retryDelayMs);
            }
    }
    }



    public void RegisterCommand<T>(string name, Func<List<string>, Task<T>> action)
    {
        _asyncCommands[name] = async (args) => await action(args);
    }

    public async Task StartListeningAsync()
    {
        FusionLogger.Log($"[Client {DisplayName}] Listening on {_clientPipeName}");

        while (true)
        {
            using var pipeServer = new NamedPipeServerStream(
                _clientPipeName,
                PipeDirection.InOut,
                1,
                PipeTransmissionMode.Message,
                PipeOptions.Asynchronous);

            await pipeServer.WaitForConnectionAsync();

            byte[] buffer = new byte[1024];
            int bytesRead = await pipeServer.ReadAsync(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            if (message.Trim() == "ping")
            {
                byte[] pong = Encoding.UTF8.GetBytes("pong");
                await pipeServer.WriteAsync(pong, 0, pong.Length);
                continue;
            }

            FusionLogger.Log($"[Client {DisplayName}] Received: {message}");

            var split = message.Split(' ');
            string commandName = split[0];
            var args = new List<string>(split.Length > 1 ? split[1..] : Array.Empty<string>());

            if (CommandManager.TryExecute(commandName, args, out var resultTask))
            {
                var result = await resultTask;
                string json = System.Text.Json.JsonSerializer.Serialize(result);
                byte[] responseBytes = Encoding.UTF8.GetBytes(json);
                await pipeServer.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
            else
            {
                FusionLogger.Log($"[Client {DisplayName}] Unknown command: {commandName}");
                byte[] responseBytes = Encoding.UTF8.GetBytes("Command received!");
                await pipeServer.WriteAsync(responseBytes, 0, responseBytes.Length);
            }

            pipeServer.Disconnect();
        }
    }

    public async Task SendMessageAsync(string message)
    {
        using var pipeClient = new NamedPipeClientStream(".", _serverPipeName, PipeDirection.InOut);
        await pipeClient.ConnectAsync();

        byte[] buffer = Encoding.UTF8.GetBytes($"{UniqueId} {message}");
        await pipeClient.WriteAsync(buffer, 0, buffer.Length);

        byte[] responseBuffer = new byte[256];
        int bytesRead = await pipeClient.ReadAsync(responseBuffer, 0, responseBuffer.Length);
        string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
        FusionLogger.Log($"[Client {DisplayName}] Server responded: {response}");
    }
}
