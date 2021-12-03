using Akavache;
using DynamicData;

namespace ;

public class AkavacheStorage : IStorage
{
    private readonly IBlobCache blob;
    private readonly Dictionary<Type, object> storages;

    public AkavacheStorage(IBlobCache blob)
    {
        this.blob = blob;
        storages = new();
    }

    /// <inheritdoc/>
    public IObservable<Unit> Clear()
    {
        return blob.InvalidateAll();
    }

    /// <inheritdoc/>
    public IStorage<T> For<T>() where T : IEntity
    {
        var type = typeof(T);
        if (storages.TryGetValue(type, out var storage))
        {
            return (IStorage<T>)storage;
        }

        storage = new AkavacheStorage<T>(blob);
        storages[type] = storage;
        return (IStorage<T>)storage;
    }
}

public class AkavacheStorage<T> : IStorage<T> where T : IEntity
{
    private readonly IBlobCache blob;
    private readonly SourceCache<T, string> cache;

    /// <inheritdoc/>
    public IObservable<int> CountChanged => cache.CountChanged;

    public AkavacheStorage(IBlobCache blob)
    {
        this.blob = blob;
        cache = new SourceCache<T, string>(static e => e.Id);
    }

    /// <inheritdoc/>
    public IObservable<IChangeSet<T, string>> Connect(Func<T, bool>? predicate = null, bool suppressEmptyChangeSets = true)
    {
        blob.GetAllObjects<T>().Subscribe(cache.AddOrUpdate);

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
        if (!cache.Lookup(key).HasValue)
        {
            blob.GetObject<T>(key).Subscribe(cache!.AddOrUpdate);
        }

        return cache.Watch(key);
    }

    /// <inheritdoc/>
    public IObservable<T[]> Get()
    {
        return blob.GetAllObjects<T>().Select(x => x.ToArray());
    }

    /// <inheritdoc/>
    public IObservable<T?> Get(string key)
    {
        var lookup = cache.Lookup(key);

        return lookup.HasValue
            ? Observable.Return(lookup.Value)
            : blob.GetObject<T>(key).Do(item => cache!.AddOrUpdate(item));
    }

    /// <inheritdoc/>
    public IObservable<Unit> Set(T item)
    {
        item.Updated = DateTimeOffset.UtcNow;

        return blob.InsertObject(item.Id, item).Do(_ => cache.AddOrUpdate(item));
    }

    /// <inheritdoc/>
    public IObservable<Unit> Set(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            item.Updated = DateTimeOffset.UtcNow;
        }

        return blob.InsertObjects(items.ToDictionary(e => e.Id)).Do(_ => cache!.AddOrUpdate(items));
    }

    /// <inheritdoc/>
    public IObservable<Unit> Remove(string key)
    {
        return blob.InvalidateObject<T>(key).Do(_ => cache!.Remove(key));
    }

    /// <inheritdoc/>
    public IObservable<Unit> Remove(IEnumerable<string> keys)
    {
        return keys.Select(k => Remove(k)).Merge().Do(_ => cache!.Remove(keys));
    }

    /// <inheritdoc/>
    public IObservable<Unit> Clear()
    {
        return blob.InvalidateAllObjects<T>().Do(_ => cache!.Clear());
    }


}
