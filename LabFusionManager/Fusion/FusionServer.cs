using System.Diagnostics;
using System.IO.Pipes;
using System.Security.Principal;
using System.Text;

public class FusionServer
{
    public string ClientId { get; private set; }
    public string DisplayName { get; private set; }
    public string ClientPipeName { get; private set; }
    public int ProcessID { get; private set; }

    private bool _isConnected = true;

    public FusionServer(string clientId, string displayName, string clientPipeName, int procID)
    {
        ClientId = clientId;
        DisplayName = displayName;
        ClientPipeName = clientPipeName;
        ProcessID = procID;
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
        using var clientPipe = new NamedPipeClientStream(".", ClientPipeName, PipeDirection.InOut,
            PipeOptions.Asynchronous, TokenImpersonationLevel.Impersonation);
        await clientPipe.ConnectAsync();
        clientPipe.ReadMode = PipeTransmissionMode.Message;

        var buffer = Encoding.UTF8.GetBytes(message);
        await clientPipe.WriteAsync(buffer, 0, buffer.Length);
        await clientPipe.FlushAsync();

        var repBuffer = new byte[1024];
        using var ms = new MemoryStream();
        do
        {
            int bytesRead = await clientPipe.ReadAsync(repBuffer, 0, repBuffer.Length);
            ms.Write(repBuffer, 0, bytesRead);
        } while (!clientPipe.IsMessageComplete);

        string response = Encoding.UTF8.GetString(ms.ToArray());
        ServerCLI.AddLog($"[Server] Client {DisplayName} responded: {response}");
    }


    public void AutoTrimMemory(long thresholdBytes = 1L * 1024 * 1024 * 1024) // default 1 GB
    {
        try
        {
            using var proc = Process.GetProcessById(ProcessID);
            long workingSet = proc.WorkingSet64;

            if (workingSet > thresholdBytes)
            {
                if (TrimMemory())
                {
                    ServerCLI.AddLog($"[Trim] {DisplayName} trimmed. Was using {workingSet / (1024 * 1024)} MB.");
                }
            }
        }
        catch
        {
            // ignore if process not found
        }
    }

    public bool TrimMemory()
    {
        return WorkingSet.TrimProcessMemory(ProcessID);
    }
}
