namespace ;

/// <summary>
/// A class that also represents an exception can be returned and or thrown.
/// </summary>
public record Error
{
    public static JsonSerializerOptions JsonOptions { get; } = new()
    {
        WriteIndented = true
    };

    /// <summary>
    /// Initialize a new <see cref="Error"/> class.
    /// </summary>
    /// <param name="title">The error's title.</param>
    /// <param name="status">The error's status code.</param>
    /// <param name="detail">A detailed message for the error.</param>
    /// <param name="metadata">A dictionary of extra data.</param>
    public Error(string title, string status, string detail, Dictionary<string, string>? metadata = null)
    {
        Title = title;
        Status = status;
        Detail = detail;
        Metadata = metadata ?? new();
    }

    public Error(Exception exception)
    {
        if (exception is ErrorException errorException)
        {
            (Title, Status, Detail, Metadata) = errorException.Error;
            return;
        }

        Title = "Unknown Error";
        Status = "-1";
        Detail = exception.Message;
        Metadata = new()
        {
            ["HResult"] = exception.HResult.ToString(),
            ["HelpLink"] = exception.HelpLink ?? string.Empty,
            ["Source"] = exception.Source ?? string.Empty,
            ["StackTrace"] = exception.StackTrace ?? string.Empty,
            ["TargetSite"] = exception.TargetSite?.Name ?? string.Empty
        };
    }

    /// <summary>
    /// A status code for the error.
    /// </summary>
    public string Status { get; init; }

    /// <summary>
    /// The error's tittle.
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// More detailed information about the error.
    /// </summary>
    public string Detail { get; init; }

    /// <summary>
    /// Other metadata for the error.
    /// </summary>
    public Dictionary<string, string> Metadata { get; init; }

    /// <summary>
    /// Get a json indented representation of the error.
    /// </summary>
    /// <returns>A json indented representation of the error.</returns>
    public override string ToString() =>
        ToString(JsonOptions);

    /// <summary>
    /// Get a json representation of the error.
    /// </summary>
    /// <param name="options">Json Serialization options.</param>
    /// <returns>A json representation of the error.</returns>
    public string ToString(JsonSerializerOptions options) =>
        JsonSerializer.Serialize(this, options);

    public void Deconstruct(out string title, out string status)
    {
        title = Title;
        status = Status;
    }

    public void Deconstruct(out string title, out string status, out string detail)
    {
        Deconstruct(out title, out status);
        detail = Detail;
    }

    public void Deconstruct(out string title, out string status, out string detail, out Dictionary<string, string> metadata)
    {
        Deconstruct(out title, out status, out detail);
        metadata = Metadata;
    }
}

public class ErrorException : Exception
{
    public Error Error { get; }

    public ErrorException(Error error) : base($"{error.Status}:{error.Title} - {error.Detail}")
    {
        Error = error;
    }

    public ErrorException(Error error, Exception inner) : base($"{error.Status}:{error.Title} - {error.Detail}", inner)
    {
        Error = error;
    }
}