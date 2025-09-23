using System.IO.Pipes;
using System.Text;

public class ServerManager
{
    public static ServerManager Instance { get; } = new ServerManager();
    private Dictionary<string, FusionServer> _clients = new();

    private const string ServerPipeName = "LabFusionServerPipe";       // where clients send messages
    private const string RegistrationPipeName = "LabFusionRegistrationPipe"; // where clients register

    private ServerManager() { }

    public IEnumerable<FusionServer> GetAllServers() => _clients.Values;

    public async Task StartRegistrationPipeAsync()
    {
        while (true)
        {
            using var regPipe = new NamedPipeServerStream(RegistrationPipeName, PipeDirection.InOut, 10, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            Console.WriteLine("[Manager] Waiting for client registration...");
            await regPipe.WaitForConnectionAsync();

            byte[] buffer = new byte[1024];
            int bytesRead = await regPipe.ReadAsync(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var parts = message.Split(':');
            if (parts.Length != 3)
            {
                Console.WriteLine("[Manager] Invalid registration message: " + message);
                continue;
            }

            string displayName = parts[0];
            string uniqueId = parts[1];
            string clientPipe = parts[2];

            if (!_clients.ContainsKey(uniqueId))
            {
                var server = new FusionServer(uniqueId, displayName, clientPipe);
                _clients.Add(uniqueId, server);
                Console.WriteLine($"[Manager] Registered client {displayName} ({uniqueId}) with pipe {clientPipe}");
            }
            else
            {
                Console.WriteLine($"[Manager] Client {displayName} ({uniqueId}) already registered");
            }

            byte[] reply = Encoding.UTF8.GetBytes("Registered");
            await regPipe.WriteAsync(reply, 0, reply.Length);
        }
    }

    public async Task StartServerPipeAsync()
    {
        while (true)
        {
            using var serverPipe = new NamedPipeServerStream(ServerPipeName, PipeDirection.InOut, 10, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            await serverPipe.WaitForConnectionAsync();

            byte[] buffer = new byte[1024];
            int bytesRead = await serverPipe.ReadAsync(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var split = message.Split(' ');
            if (split.Length < 1) continue;

            string uniqueId = split[0];
            string clientMessage = string.Join(' ', split[1..]);
            var splitMessage = clientMessage.Split(' ', 2);
            string commandName = splitMessage[0];

            var allowedCommands = ServerCLI.GetRegisteredCommandNames();

            if (_clients.TryGetValue(uniqueId, out var client))
            {
                if (allowedCommands.Contains(commandName))
                {
                    await client.SendMessageToClientAsync(clientMessage);
                }
                else
                {
                    Console.WriteLine($"[Manager] Blocked unknown command '{commandName}'.");
                }
            }

        }
    }

    public async Task StartPingLoopAsync()
    {
        while (true)
        {
            var clients = _clients.Values.ToList();
            var tasks = clients.Select(async client =>
            {
                bool alive = await client.PingAsync();
                return (client, alive);
            }).ToList();

            var results = await Task.WhenAll(tasks);

            foreach (var (client, alive) in results)
            {
                if (!alive)
                {
                    Console.WriteLine($"[Manager] Client {client.DisplayName} ({client.ClientId}) disconnected.");
                    _clients.Remove(client.ClientId);
                }
            }

            await Task.Delay(1000);
        }
    }

}