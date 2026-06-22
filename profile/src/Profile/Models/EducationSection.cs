namespace Profile.Models;

using Profile.Abstract;
using System;

public sealed record EducationSection : ResumeSection<EducationSection>
{
    public Education[] Items { get; init; } = [];

    public string? DateFormat { get; init; }
}

public sealed record Education
{
    public required string Institution { get; init; }

    public required DateOnly StartDate { get; init; }

    public DateOnly? EndDate { get; init; }

    public string? Degree { get; init; }

    public string? Location { get; init; }

    public EducationInfo[] Information { get; init; } = [];
}

public record EducationInfo
{
    public string? Title { get; init; }

    public string? Description { get; init; }

    public string? Url { get; init; }
}
