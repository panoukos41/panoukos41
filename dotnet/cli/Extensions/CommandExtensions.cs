using System.CommandLine;

namespace Proxy.Manager.Extensions;

public static class Extensions
{
    public static void AddCommands(this Command root, params Command[] commands)
    {
        foreach (var command in commands)
        {
            root.AddCommand(command);
        }
    }
}
