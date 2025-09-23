_ = ServerManager.Instance.StartRegistrationPipeAsync();
_ = ServerManager.Instance.StartPingLoopAsync();
Console.WriteLine("Server Manager ready. Registration pipe open.");
await ServerCLI.StartLiveCommandCLIAsync();