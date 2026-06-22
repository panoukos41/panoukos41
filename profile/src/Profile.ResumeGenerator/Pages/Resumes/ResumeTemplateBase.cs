using Microsoft.AspNetCore.Components;
using Profile.Models;
using Profile.ResumeGenerator.Common;

namespace Profile.ResumeGenerator.Pages.Resumes;

public abstract class ResumeTemplateBase : ComponentBase
{
    [Parameter, EditorRequired]
    public Resume Resume { get; set; } = null!;

    [Parameter, EditorRequired]
    public string ResumeName { get; set; } = null!;

    [Parameter]
    public string? Lang { get; set; } = null!;

    [Inject]
    public ApplicationContext Context { get; set; } = null!;
}
