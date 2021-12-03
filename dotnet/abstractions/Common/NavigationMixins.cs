namespace ;

/// <summary>
/// Allows an object to define it's route.
/// </summary>
public interface IRoute
{
    /// <summary>
    /// The object's navigation route.
    /// </summary>
    static abstract string Route { get; }
}

/// <summary>
/// Allows an object to define multiple routes.
/// </summary>
public interface IRoutes
{
    /// <summary>
    /// The object's navigation routes.
    /// </summary>
    static abstract string[] Routes { get; }
}
