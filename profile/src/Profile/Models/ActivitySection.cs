using Profile.Abstract;

namespace Profile.Models;

public sealed record ActivitySection : ResumeSection<ActivitySection>
{
    public string[] Items { get; init; } = [];
}
