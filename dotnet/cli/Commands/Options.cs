using System.CommandLine;

namespace Proxy.Manager.Commands;

/// <summary>
/// Contains multiple <see cref="Option{T}"/> items.
/// </summary>
public static class Options
{
    public static Option<string> ProxyPath { get; } = new(
        name: "--proxy",
        description: "The path to the Proxy.exe file default(CurrentDirectory).",
        getDefaultValue: () => Path.Combine(Environment.CurrentDirectory, "Proxy.exe")
    );

    public static Option<string> DisplayName { get; } = new(
        name: "--display",
        description: "The service display name.",
        getDefaultValue: () => "Proxy made with YARP"
    );

    public static Option<string> ServiceName { get; } = new(
        name: "--name",
        description: "The service name, default(Proxy)",
        getDefaultValue: () => "Proxy"
    );
}
