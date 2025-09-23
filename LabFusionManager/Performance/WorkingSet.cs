using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

internal static class WorkingSet
{
    [DllImport("psapi.dll")]
    static extern bool EmptyWorkingSet(IntPtr hProcess);

    public static bool TrimProcessMemory(int pid)
    {
        try
        {
            using var proc = Process.GetProcessById(pid);
            return EmptyWorkingSet(proc.Handle);
        }
        catch
        {
            return false;
        }
    }
}
