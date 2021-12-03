using DynamicData;

namespace ;

/// <summary>
/// Store items into different collections.
/// </summary>
public interface IStorage
{
    /// <summary>
    /// Get a collection for the provided key.
    /// </summary>
    /// <typeparam name="T">The collections items type and name.</typeparam>
    public IStorage<T> For<T>() where T : IEntity;

    /// <summary>
    /// Clear all collections.
    /// </summary>
    public IObservable<Unit> Clear();
}

/// <summary>
/// Add, remove, search a collection of items.
/// </summary>
/// <typeparam name="T">The type of the items.</typeparam>
public interface IStorage<T> : IConnectableCache<T, string> where T : IEntity
{
    public IObservable<T?> this[string key]
    {
        get => Get(key);
    }

    /// <summary>
    /// Get all items in the collection.
    /// </summary>
    /// <returns>An observable that when ticked contains all the items of the collection.</returns>
    public IObservable<T[]> Get();

    /// <summary>
    /// Get an item for the specified key or null if not found.
    /// </summary>
    /// <param name="key">The key to search for.</param>
    /// <returns>An observable that when ticked contains the item or null if not found.</returns>
    public IObservable<T?> Get(string key);

    /// <summary>
    /// Store an item with the specified key.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>An observable that ticks when the operation is done.</returns>
    public IObservable<Unit> Set(T item);

    /// <summary>
    /// Cache multiple items.
    /// </summary>
    /// <param name="items">The key/item pairs to store.</param>
    /// <returns>An observable that ticks when the operation is done.</returns>
    public IObservable<Unit> Set(IEnumerable<T> items);

    /// <summary>
    /// Remove an item from the collection.
    /// </summary>
    /// <param name="key">The key to remove.</param>
    /// <returns>An observable that ticks when the operation is done.</returns>
    public IObservable<Unit> Remove(string key);

    /// <summary>
    /// Remove all items from the collection.
    /// </summary>
    /// <param name="key">The keys to remove.</param>
    /// <returns>An observable that ticks when the operation is done.</returns>
    public IObservable<Unit> Remove(IEnumerable<string> key);

    /// <summary>
    /// Clear the collection.
    /// </summary>
    /// <returns>An observable that ticks when the operation is done.</returns>
    public IObservable<Unit> Clear();
}
