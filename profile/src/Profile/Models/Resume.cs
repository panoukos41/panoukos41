namespace Profile.Models;

public sealed record Resume
{
    public required ResumeDetails Details { get; init; }

    public required ContactInfoSection Contact { get; init; }

    public ExperienceSection Experience { get; init; } = ExperienceSection.Empty;

    public EducationSection Education { get; init; } = EducationSection.Empty;

    public CertificationSection Certifications { get; init; } = CertificationSection.Empty;

    public LanguageSection Languages { get; init; } = LanguageSection.Empty;

    public ProjectSection Projects { get; init; } = ProjectSection.Empty;

    public SkillSection Skills { get; init; } = SkillSection.Empty;

    public SkillSection SoftSkills { get; init; } = SkillSection.Empty;

    public KnowledgeSection Knowledge { get; init; } = KnowledgeSection.Empty;

    public ActivitySection Activities { get; init; } = ActivitySection.Empty;

    public ResumeConfig Config { get; init; } = ResumeConfig.Empty;
}
