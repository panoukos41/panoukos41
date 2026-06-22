using Flurl;
using Profile.Models;

namespace Profile.ResumeGenerator.Pages.Resumes;

public sealed class ResumeLoader
{
    private readonly string resumesDir;
    private readonly JsonSerializerOptions jsonOptions;

    public ResumeLoader(string resumesDir)
    {
        this.resumesDir = Path.GetFullPath(resumesDir);
        jsonOptions = new(JsonSerializerOptions.Default)
        {
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };
    }

    public static string GetPhotoUrl(string name, string? lang)
    {
        return $"api/loader/{name}".SetQueryParam(nameof(lang), lang);
    }

    public async Task<Resume> GetResume(string name, string? lang, CancellationToken cancellationToken = default)
    {
        var file = GetFile("resume", "json", name, lang);

        using var stream = File.OpenRead(file);
        var resume = await JsonSerializer.DeserializeAsync<Resume>(stream, jsonOptions, cancellationToken);

        ArgumentNullException.ThrowIfNull(resume);
        return resume;
    }

    public Stream GetPhoto(string name, string? lang)
    {
        var file = GetFile("photo", "png", name, lang);
        return File.OpenRead(file);
    }

    private string GetFile(string file, string extension, string name, string? lang)
    {
        return lang is null
            ? Path.Combine(resumesDir, name, $"{file}.{extension}")
            : Path.Combine(resumesDir, name, $"{file}-{lang}.{extension}");
    }
}
