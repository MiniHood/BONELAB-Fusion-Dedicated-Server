public class ServerCLI
{
    private static List<string> _log = new();

    public static async Task StartLiveCommandCLIAsync()
    {
        string currentInput = "";
        int selectedIndex = 0;

        while (true)
        {
            var clients = ServerManager.Instance.GetAllServers().ToList();
            if (clients.Count == 0)
            {
                AddLog("No clients connected yet...");
                await Task.Delay(500);
                continue;
            }

            Draw(clients, selectedIndex, currentInput);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.UpArrow)
                selectedIndex = (selectedIndex - 1 + clients.Count) % clients.Count;
            else if (keyInfo.Key == ConsoleKey.DownArrow)
                selectedIndex = (selectedIndex + 1) % clients.Count;
            else if (keyInfo.Key == ConsoleKey.Backspace)
            {
                if (currentInput.Length > 0)
                    currentInput = currentInput[..^1];
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                var client = clients[selectedIndex];

                await client.SendMessageToClientAsync(currentInput);
                AddLog($"Sent command '{currentInput}' to {client.DisplayName}");

                currentInput = "";
            }
            else
            {
                currentInput += keyInfo.KeyChar;
            }
        }
    }

    private static void Draw(List<FusionServer> clients, int selectedIndex, string input)
    {
        Console.Clear();

        int maxLines = Console.WindowHeight - 10;

        Console.WriteLine("Use ↑/↓ to scroll through clients. Type command and press Enter to send.\n");

        for (int i = 0; i < clients.Count; i++)
        {
            Console.Write(i == selectedIndex ? "> " : "  ");
            Console.WriteLine($"{clients[i].ClientId} - {clients[i].DisplayName}");
        }

        Console.WriteLine("\nCommand: " + input + "\n");

        int start = Math.Max(0, _log.Count - maxLines);
        for (int i = start; i < _log.Count; i++)
            Console.WriteLine(_log[i]);
    }

    public static void AddLog(string message)
    {
        _log.Add(message);
        if (_log.Count > 1000)
            _log.RemoveAt(0);
    }
}
