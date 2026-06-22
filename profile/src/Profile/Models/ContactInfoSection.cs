using Profile.Abstract;

namespace Profile.Models;

public sealed record ContactInfoSection : ResumeSection<ContactInfoSection>
{
    public string? Email { get; init; }

    public string? Phone { get; init; }

    public string? Address { get; init; }

    public ContactInfoWebsites[] Websites { get; init; } = [];
}

public sealed record ContactInfoWebsites
{
    public required string Title { get; init; }

    public required string Url { get; init; }

    public string? Icon { get; init; }
}
