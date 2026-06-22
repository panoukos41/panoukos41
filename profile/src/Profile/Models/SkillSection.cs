using Profile.Abstract;

namespace Profile.Models;

public sealed record SkillSection : ResumeSection<SkillSection>
{
    public int Max { get; init; } = 5;

    public int Min { get; init; }

    public Skill[] Items { get; init; } = [];
}

public sealed record Skill
{
    public required string Title { get; set; }

    public int Level { get; set; }
}
