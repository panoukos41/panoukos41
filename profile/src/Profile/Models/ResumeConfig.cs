namespace Profile.Models;

public sealed record ResumeConfig
{
    public static ResumeConfig Empty { get; } = new();

    public string Today { get; init; } = nameof(Today);

    public string? DateFormat { get; init; }
}
