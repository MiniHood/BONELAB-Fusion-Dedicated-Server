using MoonSharp.Interpreter;

public static class LuaEngine
{
    public static Script _lua;
    public static void Init()
    {
        _lua = new Script();

        UserData.RegisterType<FusionServer>();
        UserData.RegisterType<ServerManager>();

        _lua.Globals["serverManager"] = ServerManager.Instance;
    }

    public static DynValue RunScript(string code)
    {
        return _lua.DoString(code);
    }

    public static DynValue RunFile(string path)
    {
        return _lua.DoFile(path);
    }
}
