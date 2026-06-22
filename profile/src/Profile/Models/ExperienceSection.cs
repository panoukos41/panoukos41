namespace Profile.Models;

using Profile.Abstract;
using System;

public sealed record ExperienceSection : ResumeSection<ExperienceSection>
{
    public Experience[] Items { get; init; } = [];

    public string? DateFormat { get; init; }
}

public record Experience
{
    public required string JobTitle { get; init; }

    public required string Company { get; init; }

    public string? Location { get; init; }

    public required DateOnly StartDate { get; init; }

    public required DateOnly? EndDate { get; init; }

    public bool Volunteer { get; init; }

    public string? Summary { get; init; }

    public ExperienceInfo[] Information { get; init; } = [];
}

public record ExperienceInfo
{
    public string? Title { get; init; }

    public string? Description { get; init; }

    public string? Url { get; init; }
}
