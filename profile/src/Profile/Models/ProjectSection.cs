using Profile.Abstract;

namespace Profile.Models;

public record ProjectSection : ResumeSection<ProjectSection>
{
    public Project[] Items { get; init; } = [];

    public string? DateFormat { get; init; }
}

public record Project
{
    public required string Title { get; init; }

    public string? Description { get; init; }

    public string? Url { get; init; }

    public DateOnly? CreatedAt { get; init; }
}
