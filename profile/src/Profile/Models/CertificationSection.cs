using Profile.Abstract;

namespace Profile.Models;

public record CertificationSection : ResumeSection<CertificationSection>
{
    public Certification[] Items { get; init; } = [];

    public string? DateFormat { get; init; }
}

public record Certification
{
    public required string Title { get; init; }

    public required string Issuer { get; init; }

    public string? Description { get; init; }

    public string? Results { get; init; }

    public required DateOnly Date { get; init; }
}
