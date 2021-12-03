using P41.Navigation;
using System.IO;

namespace ;

public class AndroidServices : ServicesBase
{
    protected override string DbPath { get; } =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db.litedb");

    protected override INavigationHost NavigationHostFactory() =>
        new NavigationHost()
        ;
}
