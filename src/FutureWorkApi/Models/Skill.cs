namespace FutureWorkApi.Models;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Category { get; set; } // ex.: Técnica, Comportamental, Digital

    public ICollection<ProfessionalSkill> ProfessionalSkills { get; set; } =
        new List<ProfessionalSkill>();
}
