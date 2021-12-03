global using ReactiveUI;
global using ReactiveUI.Fody.Helpers;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Linq.Expressions;
global using System.Reactive;
global using System.Reactive.Linq;
global using System.Text.Json;

namespace ;

/// <summary>
/// Static application object to resolve services and access config values.
/// </summary>
/// <remarks>Call initialize before you use it.</remarks>
public static class App
{
    private static IServiceProvider? services;

    /// <summary>
    /// Initiaze app services.
    /// </summary>
    public static void Initialize(IServiceProvider services)
    {
        App.services = services;
    }

    /// <summary>
    /// Resolve a service from the services provided during initialize.
    /// </summary>
    /// <typeparam name="T">The service to resolve.</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException">When the service <typeparamref name="T"/> can't be resolved.</exception>
    /// <exception cref="InvalidOperationException">When the services container is not initialized yet.</exception>
    public static T Resolve<T>()
    {
        return services is { }
            ? services.GetService(typeof(T)) is T obj
                ? obj
                : throw new ArgumentException($"Could not resolve type: '{typeof(T)}'")
            : throw new InvalidOperationException("Please call Initialzie before trying to resolve services");
    }
}
