using ConsoleAppFramework;
using Profile.Models;
using Profile.ResumeGenerator.Common;
using Profile.ResumeGenerator.Pages;
using Profile.ResumeGenerator.Pages.Resumes;
using System.Globalization;

var app = ConsoleApp.Create();

app.Add("schema", Schema);
app.Add("run", Run);

await app.RunAsync(args);

static Task Schema(string outputFile)
{
    var options = new JsonSerializerOptions(JsonSerializerOptions.Default)
    {
        WriteIndented = true
    };

    var node = options.GetJsonSchemaAsNode(typeof(Resume));
    var schema = node.ToJsonString();

    var bytes = JsonSerializer.SerializeToUtf8Bytes(node, options);
    var file = Path.GetFullPath(outputFile);

    Console.WriteLine($"Generating schema at '{file}'");

    return File.WriteAllBytesAsync(file, bytes);
}

static Task Run(string resumesDir)
{
    var builder = WebApplication.CreateBuilder();
    builder.Services.AddRazorComponents().AddInteractiveServerComponents();
    builder.Services.AddSingleton(new ResumeLoader(resumesDir));
    builder.Services.AddSingleton(new ApplicationContext { Command = "run" });

    var app = builder.Build();

    app.UseAntiforgery();
    app.MapStaticAssets();
    app.Use((context, next) =>
    {
        var culture = context.Request.Query.TryGetValue("lang", out var lang) is false || string.IsNullOrEmpty(lang[0])
            ? CultureInfo.GetCultureInfo("el")
            : CultureInfo.GetCultureInfo(lang[0]!);

        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = culture;
        return next();
    });
    app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
    app.MapGet("api/loader/{name}", (string name, string? lang, ResumeLoader loader) =>
    {
        var stream = loader.GetPhoto(name, lang);
        return Results.Stream(stream, "image/png");
    });

    return app.RunAsync();
}
