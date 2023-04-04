using System.CommandLine;
using System.CommandLine.Binding;

namespace Proxy.Manager.Abstractions;

public abstract class OptionsBase<T> : BinderBase<T>
    where T : OptionsBase<T>, new()
{
    public abstract IValueDescriptor[] Descriptors { get; }

    private BindingContext context = null!;

    public TValue GetValue<TValue>(Option<TValue> option)
        => context.ParseResult.GetValueForOption(option)!;

    public TValue GetValue<TValue>(Argument<TValue> argument)
        => context.ParseResult.GetValueForArgument(argument)!;

    protected override T GetBoundValue(BindingContext bindingContext)
    {
        context = bindingContext;
        return (T)this;
    }
}

