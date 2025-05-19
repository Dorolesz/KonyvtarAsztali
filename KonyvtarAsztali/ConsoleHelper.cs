using System;
using System.Runtime.InteropServices;

public static class ConsoleHelper
{
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();

    public static void ShowConsole()
    {
        AllocConsole();
    }
}
