namespace Profile.ResumeGenerator.Common;

public static class Mixins
{
    public static string Join(this string original, string separator, params IEnumerable<string?> values)
    {
        values = values.Where(x => x is not null);
        return values.Any() ? string.Join(separator, [original, .. values]) : original;
    }
}

public sealed class CustomDateOnlyComparer : IComparer<DateOnly?>
{
    public static CustomDateOnlyComparer Instance { get; } = new();

    public int Compare(DateOnly? x, DateOnly? y)
    {
        if (x is null)
            return -1;
        if (y is null)
            return 1;
        return x.Value.CompareTo(y.Value);
    }
}
