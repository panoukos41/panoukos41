using DynamicData;
using DynamicData.Kernel;
using SQLite;
using System.Reactive.Disposables;

namespace ;

public class SQLiteNetPclStorage : IStorage
{
    private readonly SQLiteConnection db;
    private readonly Dictionary<Type, object> storages;

    public SQLiteNetPclStorage(SQLiteConnection db)
    {
        storages = new();
        this.db = db;

        db.CreateTable<Info>();
        db.CreateTable<Data>();

        try
        {
            db.Get<Info>(1);
        }
        catch
        {
            db.Insert(new Info() { Version = 1 });
        }
    }

    /// <inheritdoc/>
    public IObservable<Unit> Clear()
    {
        return Observable.Return(db.DeleteAll<Data>()).ToUnit();
    }

    /// <inheritdoc/>
    public IStorage<T> For<T>() where T : IEntity
    {
        var type = typeof(T);
        if (storages.TryGetValue(type, out var storage))
        {
            return (IStorage<T>)storage;
        }

        storage = new SQLiteNetPclStorage<T>(db);
        storages[type] = storage;
        return (IStorage<T>)storage;
    }

    [Table("Info")]
    public class Info
    {
        public int Version { get; set; }
    }

    [Table("Data")]
    public class Data
    {
        [PrimaryKey]
        public string Key { get; set; } = string.Empty;

        [Indexed]
        public string? TypeName { get; set; }

        public byte[] Value { get; set; } = Array.Empty<byte>();
    }
}

public class SQLiteNetPclStorage<T> : IStorage<T> where T : IEntity
{
    private readonly SQLiteConnection db;
    private readonly SourceCache<T, string> cache;
    private bool firstConnect;

    private string Query { get; }

    /// <inheritdoc/>
    public IObservable<int> CountChanged => cache.CountChanged;

    public SQLiteNetPclStorage(SQLiteConnection db)
    {
        this.db = db;
        cache = new SourceCache<T, string>(static e => e.Id);
        Query = $"SELECT Key FROM Data WHERE TypeName='{typeof(T).FullName}'";
    }

    /// <inheritdoc/>
    public IObservable<IChangeSet<T, string>> Connect(Func<T, bool>? predicate = null, bool suppressEmptyChangeSets = true)
    {
        // todo: Load all.
        if (firstConnect)
        {
            firstConnect = false;
            Get().Subscribe(items => cache.AddOrUpdate(items));
        }

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
            var data = db.Get<SQLiteNetPclStorage.Data>(key);

            cache.AddOrUpdate(JsonSerializer.Deserialize<T>(data.Value.AsSpan())!);
        }
        return cache.Watch(key);
    }

    /// <inheritdoc/>
    public IObservable<T[]> Get()
    {
        return Observable
            .Return(db.Query<SQLiteNetPclStorage.Data>(Query, Array.Empty<object>()))
            .SelectMany(static d => d)
            .Select(static d => JsonSerializer.Deserialize<T>(d.Value.AsSpan())!)
            .ToArray();
    }

    /// <inheritdoc/>
    public IObservable<T?> Get(string key)
    {
        var lookup = cache.Lookup(key);

        return lookup.HasValue
            ? Observable
                .Return(lookup.Value)
            : Observable
                .Return(db.Find<SQLiteNetPclStorage.Data>(key))
                .Select(static d => JsonSerializer.Deserialize<T>(d.Value.AsSpan()));
    }

    /// <inheritdoc/>
    public IObservable<Unit> Set(T item)
    {
        item.Updated = DateTimeOffset.UtcNow;

        var dbCache = new SQLiteNetPclStorage.Data
        {
            Key = item.Id,
            TypeName = item.GetType().FullName,
            Value = JsonSerializer.SerializeToUtf8Bytes(item)
        };

        return Observable
            .Return(db.InsertOrReplace(dbCache))
            .Do(_ => cache!.AddOrUpdate(item))
            .ToUnit();
    }

    /// <inheritdoc/>
    public IObservable<Unit> Set(IEnumerable<T> items) => Observable.Create<Unit>(o =>
    {
        var objects = items.AsArray();

        foreach (var item in objects)
        {
            item.Updated = DateTimeOffset.UtcNow;
            var dbCache = new SQLiteNetPclStorage.Data
            {
                Key = item.Id,
                TypeName = item.GetType().FullName,
                Value = JsonSerializer.SerializeToUtf8Bytes(item)
            };

            db.InsertOrReplace(dbCache);
        }

        cache!.AddOrUpdate(objects);

        o.OnNext(Unit.Default);
        o.OnCompleted();
        return Disposable.Empty;
    });

    /// <inheritdoc/>
    public IObservable<Unit> Remove(string key)
    {
        return Observable
            .Return(db.Delete<T>(key))
            .Do(_ => cache!.Remove(key))
            .ToUnit();
    }

    /// <inheritdoc/>
    public IObservable<Unit> Remove(IEnumerable<string> keys) => Observable.Create<Unit>(o =>
    {
        var objectKeys = keys.AsArray();
        foreach (var key in objectKeys)
        {
            db.Delete<T>(key);
        }
        cache!.Remove(objectKeys);

        o.OnNext(Unit.Default);
        o.OnCompleted();
        return Disposable.Empty;
    });

    /// <inheritdoc/>
    public IObservable<Unit> Clear()
    {
        return Remove(db.QueryScalars<string>(Query, Array.Empty<object>()));
    }
}
