using Akavache;
using Akavache.Sqlite3;
using P41.Navigation;
using System.IO;

namespace ;

/// <summary>Base service implementation that platform services derive from.</summary>

// Main Services
[Singleton(typeof(INavigationHost), Factory = nameof(NavigationHostFactory))]

[Singleton(typeof(IBlobCache), Factory = nameof(AkavacheFactory))]
[Singleton(typeof(IStorage), typeof(AkavacheStorage))]

// ViewModels

//
[ServiceProvider]
public abstract partial class ServicesBase
{
    /// <summary>The folder path where to store the app database.</summary>
    protected abstract string DbPath { get; }

    protected abstract INavigationHost NavigationHostFactory();

    private IBlobCache AkavacheFactory()
    {
        return new SQLitePersistentBlobCache(Path.Combine(DbPath, "shopping_list.sqlite3"));
    }
}
