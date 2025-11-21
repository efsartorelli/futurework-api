namespace FutureWorkApi.Models;

public class Professional
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    // Área de interesse no futuro do trabalho (ex.: IA, Dados, Cloud)
    public string FutureArea { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ProfessionalSkill> ProfessionalSkills { get; set; } =
        new List<ProfessionalSkill>();
}
