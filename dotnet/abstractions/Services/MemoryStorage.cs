using DynamicData;

namespace ;

public class MemoryStorage : IStorage
{
    private readonly Dictionary<Type, object> storages;

    public MemoryStorage()
    {
        storages = new();
    }

    /// <inheritdoc/>
    public IObservable<Unit> Clear()
    {
        storages.ToObservable().Subscribe(pair =>
        {
            var type = pair.Key;

            var storage = type.GetMethod("Clear");
        });
        return Observable.Return(Unit.Default);
    }

    /// <inheritdoc/>
    public IStorage<T> For<T>() where T : IEntity
    {
        var type = typeof(T);
        if (storages.TryGetValue(type, out var storage))
        {
            return (IStorage<T>)storage;
        }

        storage = new MemoryStorage<T>();
        storages[type] = storage;
        return (IStorage<T>)storage;
    }
}

public class MemoryStorage<T> : IStorage<T> where T : IEntity
{
    private readonly SourceCache<T, string> cache;

    /// <inheritdoc/>
    public IObservable<int> CountChanged => cache.CountChanged;

    public MemoryStorage()
    {
        cache = new SourceCache<T, string>(static e => e.Id);
    }

    /// <inheritdoc/>
    public IObservable<IChangeSet<T, string>> Connect(Func<T, bool>? predicate = null, bool suppressEmptyChangeSets = true)
    {
        return cache.Connect(predicate, suppressEmptyChangeSets);
    }

    /// <inheritdoc/>
    public IObservable<IChangeSet<T, string>> Preview(Func<T, bool>? predicate = null)
    {
        return cache.Preview(predicate);
    }

    /// <inheritdoc/>
    public IObservable<Change<T, string>> Watch(string key)
    {
        return cache.Watch(key);
    }

    /// <inheritdoc/>
    public IObservable<T[]> Get()
    {
        return Observable.Return(cache.Items.ToArray());
    }

    /// <inheritdoc/>
    public IObservable<T?> Get(string key)
    {
        return Observable.Return(cache.Lookup(key).Value);
    }

    /// <inheritdoc/>
    public IObservable<Unit> Set(T item)
    {
        item.Updated = DateTimeOffset.UtcNow;

        return Observable.Return(Unit.Default).Do(_ => cache.AddOrUpdate(item));
    }

    /// <inheritdoc/>
    public IObservable<Unit> Set(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            item.Updated = DateTimeOffset.UtcNow;
        }

        return Observable.Return(Unit.Default).Do(_ => cache!.AddOrUpdate(items));
    }

    /// <inheritdoc/>
    public IObservable<Unit> Remove(string key)
    {
        return Observable.Return(Unit.Default).Do(_ => cache!.Remove(key));
    }

    /// <inheritdoc/>
    public IObservable<Unit> Remove(IEnumerable<string> keys)
    {
        return Observable.Return(Unit.Default).Do(_ => cache!.Remove(keys));
    }

    /// <inheritdoc/>
    public IObservable<Unit> Clear()
    {
        return Observable.Return(Unit.Default).Do(_ => cache!.Clear());
    }


}
