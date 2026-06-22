namespace Profile.Abstract;

public abstract record ResumeSection<TSelf> where TSelf : ResumeSection<TSelf>, new()
{
    public static TSelf Empty { get; } = new() { Title = typeof(TSelf).Name };

    public string Title { get; init; } = string.Empty;
}
