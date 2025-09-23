using MoonSharp.Interpreter;

// Init lua engine
UserData.RegisterType<LuaServerAPI>();
LuaEngine.Lua().Globals["api"] = new LuaServerAPI();

// start pipes and server
﻿_ = ServerManager.Instance.StartRegistrationPipeAsync();
_ = ServerManager.Instance.StartPingLoopAsync();
Console.WriteLine("Server Manager ready. Registration pipe open.");
await ServerCLI.StartLiveCommandCLIAsync();