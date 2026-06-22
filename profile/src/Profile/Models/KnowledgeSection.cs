using Profile.Abstract;

namespace Profile.Models;

public sealed record KnowledgeSection : ResumeSection<KnowledgeSection>
{
    public string[] Items { get; init; } = [];
}
