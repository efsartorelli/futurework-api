using FutureWorkApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutureWorkApi.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProfessionalController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProfessionalController(AppDbContext context)
    {
        _context = context;
    }

    // GET api/v2/professional/5/recommendations
    [HttpGet("{id:int}/recommendations")]
    public async Task<IActionResult> GetRecommendations(int id)
    {
        var prof = await _context.Professionals.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (prof == null)
            return NotFound();

        var area = prof.FutureArea?.ToLower() ?? string.Empty;
        string[] skills;

        if (area.Contains("dados"))
        {
            skills = new[]
            {
                "Modelagem de Dados",
                "SQL Avançado",
                "Machine Learning Básico",
                "Ferramentas de BI"
            };
        }
        else if (area.Contains("ia") || area.Contains("inteligência"))
        {
            skills = new[]
            {
                "Fundamentos de IA",
                "Redes Neurais",
                "Prompt Engineering",
                "Ética em IA"
            };
        }
        else if (area.Contains("cloud") || area.Contains("nuvem"))
        {
            skills = new[]
            {
                "Arquitetura Cloud",
                "Containers e Kubernetes",
                "DevOps e CI/CD",
                "Segurança em Cloud"
            };
        }
        else
        {
            skills = new[]
            {
                "Pensamento Crítico",
                "Comunicação Colaborativa",
                "Aprendizagem Contínua",
                "Resolução de Problemas"
            };
        }

        var response = new
        {
            ProfessionalId = prof.Id,
            prof.FullName,
            prof.FutureArea,
            RecommendedSkills = skills
        };

        return Ok(response);
    }
}
