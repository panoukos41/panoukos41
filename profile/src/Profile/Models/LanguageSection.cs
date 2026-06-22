using Profile.Abstract;

namespace Profile.Models;

public sealed record LanguageSection : ResumeSection<LanguageSection>
{
    public Language[] Items { get; init; } = [];
}

public record Language
{
    public required string Name { get; init; }

    public required string Proficiency { get; init; }
}
