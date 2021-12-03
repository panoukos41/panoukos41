using AndroidX.Fragment.App;

namespace P41.Navigation;

public static class NavigationMixins
{
    /// <summary>
    /// Map the Route of <typeparamref name="T"/> to the current <paramref name="host"/>.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="IRoute"/>.</typeparam>
    /// <param name="host">The host to configure.</param>
    /// <param name="vm">The ViewModel factory method.</param>
    /// <param name="view">The View factory method.</param>
    /// <returns>The host.</returns>
    public static NavigationHost MapRoute<T>(this NavigationHost host, Func<object>? vm, Func<Fragment> view)
        where T : IRoute
    {
        return host.Map(T.Route, vm, view);
    }

    /// <summary>
    /// Maps all Routes of <typeparamref name="T"/> to the current <paramref name="host"/>.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="IRoutes"/>.</typeparam>
    /// <param name="host">The host to configure.</param>
    /// <param name="vm">The ViewModel factory method.</param>
    /// <param name="view">The View factory method.</param>
    /// <returns>The host.</returns>
    public static NavigationHost MapRoutes<T>(this NavigationHost host, Func<object>? vm, Func<Fragment> view)
        where T : IRoutes
    {
        for (int i = 0; i < T.Routes.Length; i++)
        {
            host.Map(T.Routes[i], vm, view);
        }
        return host;
    }
}