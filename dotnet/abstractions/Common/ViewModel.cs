using P41.Navigation;

namespace ;

/// <summary>
/// Base implementation of a ViewModel.
/// </summary>
public abstract class ViewModel : ReactiveObject, IActivatableViewModel, INavigationAware
{
    /// <inheritdoc/>
    public virtual ViewModelActivator Activator { get; } = new();

    /// <summary></summary>
    [Reactive]
    public Error? Error { get; private set; }

    /// <summary></summary>
    protected ViewModel()
    {
        Activator.Activated.Subscribe(Activated);
        Activator.Deactivated.Subscribe(Deactivated);
    }

    /// <summary>
    /// Executed when the View is activated.
    /// </summary>
    public virtual void Activated(Unit obj)
    {
    }

    /// <summary>
    /// Executed when the View is deactivated.
    /// </summary>
    public virtual void Deactivated(Unit obj)
    {
    }

    /// <inheritdoc/>
    public virtual IObservable<Unit> NavigatedTo(Url request, INavigationHost host)
    {
        return Observable.Return(Unit.Default);
    }

    /// <inheritdoc/>
    public virtual IObservable<Unit> NavigatingFrom()
    {
        return Observable.Return(Unit.Default);
    }
}
