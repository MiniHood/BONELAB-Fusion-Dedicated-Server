using System;
using System.Collections.Generic;

public class Command
{
    public string Name { get; private set; }
    public Action<List<string>> Execute { get; private set; }

    public Command(string name, Action<List<string>> execute)
    {
        Name = name;
        Execute = execute;
    }
}
