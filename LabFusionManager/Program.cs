using MoonSharp.Interpreter;
using System.Runtime.InteropServices;

// Init lua engine
//UserData.RegisterType<LuaServerAPI>();
//LuaEngine._lua.Globals["api"] = new LuaServerAPI();

// start pipes and server
_ = ServerManager.Instance.StartRegistrationPipeAsync();
_ = ServerManager.Instance.StartPingLoopAsync();
_ = ServerManager.Instance.StartMemoryTrimLoopAsync();
Console.WriteLine("Server Manager ready. Registration pipe open.");

// start CLI
await ServerCLI.StartLiveCommandCLIAsync();