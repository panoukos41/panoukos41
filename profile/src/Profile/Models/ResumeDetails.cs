namespace Profile.Models;

public sealed record ResumeDetails
{
    public required string Title { get; init; }

    public required string Name { get; init; }

    public string? JobTitle { get; init; }

    public bool Photo { get; init; }

    public string? Summary { get; init; }
}
