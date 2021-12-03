using FluentValidation.Results;

namespace ;

/// <summary>
/// Base implementation of a ViewModel.
/// </summary>
public abstract class ValidatableViewModel<TModel> : ReactiveObject, IActivatableViewModel
    where TModel : IValidatableEntity
{
    private readonly bool rulesSet = false;

    /// <inheritdoc/>
    public virtual ViewModelActivator Activator { get; } = new();

    /// <summary>
    /// The model to be validated.
    /// </summary>
    [Reactive]
    public TModel? Model { get; protected set; }

    /// <summary>
    /// The validation rules of the model.
    /// </summary>
    [Reactive]
    public IValidationRule[]? Rules { get; private set; }

    /// <summary>
    /// The result of validation.
    /// </summary>
    [Reactive]
    public ValidationResult? Result { get; private set; }

    /// <summary></summary>
    [Reactive]
    public Error? Error { get; private set; }

    /// <summary></summary>
    protected ValidatableViewModel()
    {
        Activator.Activated.Subscribe(Activated);
        Activator.Deactivated.Subscribe(Deactivated);

        this.WhenAnyValue(x => x.Model)
            .WhereNotNull()
            .Do(x =>
            {
                if (!rulesSet) Rules = x.GetRules();
            })
            .Select(x => x.Validate())
            .BindTo(this, x => x.Result);
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
}
