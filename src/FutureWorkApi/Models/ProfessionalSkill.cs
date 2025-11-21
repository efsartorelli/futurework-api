namespace FutureWorkApi.Models;

public class ProfessionalSkill
{
    public int ProfessionalId { get; set; }
    public Professional Professional { get; set; } = null!;

    public int SkillId { get; set; }
    public Skill Skill { get; set; } = null!;

    // Nível de proficiência (1–5, por exemplo)
    public int ProficiencyLevel { get; set; } = 1;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
