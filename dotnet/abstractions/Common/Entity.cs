using FluentValidation.Results;
using System.ComponentModel;
using System.IO;

namespace ;

/// <summary>
/// Defines an entity object.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// This objects uuid.
    /// </summary>
    string Id { get; init; }

    /// <summary>
    /// The exact momment this object was created.
    /// </summary>
    DateTimeOffset Created { get; init; }

    /// <summary>
    /// The exact momment this object was updated.
    /// </summary>
    DateTimeOffset Updated { get; set; }
}

/// <summary>
/// Provides a way for an object to be validated. 
/// </summary>
public interface IValidatableEntity
{
    /// <summary>
    /// Get the rules for validation.
    /// </summary>
    /// <returns>
    /// The validation rules. If none are found or the 
    /// class is not supported an empty array is returned.
    /// </returns>
    IValidationRule[] GetRules();

    /// <summary>
    /// Validates the object.
    /// </summary>
    ValidationResult Validate();
}

/// <summary>
/// Base entity type to define entities. <br/>
/// Implements: <see cref="IEntity"/> <br/>
/// </summary>
public abstract record Entity : IEntity, IReactiveObject
{
    /// <summary>
    /// Initialize a new Entity with Created equal to UtcNow.
    /// </summary>
    protected Entity()
    {
        Id = Path.GetRandomFileName().Remove(8, 1);
        Created = DateTimeOffset.UtcNow;
        Updated = Created;
    }

    /// <inheritdoc/>
    public string Id { get; init; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; init; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <inheritdoc/>
    public event PropertyChangingEventHandler? PropertyChanging;

    /// <inheritdoc/>
    public void RaisePropertyChanged(PropertyChangedEventArgs args) => PropertyChanged?.Invoke(this, args);

    /// <inheritdoc/>
    public void RaisePropertyChanging(PropertyChangingEventArgs args) => PropertyChanging?.Invoke(this, args);
}

/// <summary>
/// Base entity type to define entities with validation. <br/>
/// Implements: <see cref="IEntity"/> <br/>
/// Implements: <see cref="IValidatableEntity"/> <br/>
/// </summary>
/// <typeparam name="TEntity">The type of the entity that inherits this and will be validated.</typeparam>
public abstract record Entity<TEntity> : Entity, IValidatableEntity
    where TEntity : Entity<TEntity>
{
    private static readonly Lazy<AbstractValidator<TEntity>> _validator =
        new(static () => App.Resolve<AbstractValidator<TEntity>>());

    private static readonly Lazy<IValidationRule[]> _rules =
        new(static () => _validator.Value.ToArray());

    /// <inheritdoc/>
    public IValidationRule[] GetRules() => _rules.Value;

    /// <inheritdoc/>
    public ValidationResult Validate() => _validator.Value.Validate((TEntity)this);
}
