namespace FutureWorkApi.Models;

public class Career
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Nível de demanda no futuro (1–5, por exemplo)
    public int FutureDemandLevel { get; set; } = 3;

    // Texto livre com overview das skills necessárias
    public string? RequiredSkillsOverview { get; set; }
}
