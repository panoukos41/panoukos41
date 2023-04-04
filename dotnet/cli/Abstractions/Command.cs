using System.CommandLine;

namespace Proxy.Manager.Abstractions;

public sealed class Command<TOptions> : Command
    where TOptions : OptionsBase<TOptions>, new()
{
    public TOptions Configure { get; }

    public Command(
        string name,
        string description,
        Action<TOptions> handler)
        : base(name, description)
    {
        Configure = new();

        foreach (var symbol in Configure.Descriptors)
        {
            if (symbol is Option option)
            {
                AddOption(option);
            }
            else if (symbol is Argument argument)
            {
                AddArgument(argument);
            }
        }

        this.SetHandler(handler, Configure);
    }

    public Command(
        string name,
        string description,
        Func<TOptions, Task> handler)
        : base(name, description)
    {
        Configure = new();

        foreach (var symbol in Configure.Descriptors)
        {
            if (symbol is Option option)
            {
                AddOption(option);
            }
            else if (symbol is Argument argument)
            {
                AddArgument(argument);
            }
        }

        this.SetHandler(handler, Configure);
    }
}
