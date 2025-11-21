using FutureWorkApi.Data;
using FutureWorkApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutureWorkApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SkillsController(AppDbContext context)
    {
        _context = context;
    }

    // GET api/v1/skills
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Skill>>> GetAll()
    {
        var list = await _context.Skills.AsNoTracking().ToListAsync();
        return Ok(list);
    }

    // GET api/v1/skills/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Skill>> GetById(int id)
    {
        var skill = await _context.Skills.FindAsync(id);
        if (skill == null)
            return NotFound();

        return Ok(skill);
    }

    // POST api/v1/skills
    [HttpPost]
    public async Task<ActionResult<Skill>> Create(Skill skill)
    {
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById),
            new { id = skill.Id, version = "1.0" }, skill);
    }

    // PUT api/v1/skills/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Skill update)
    {
        if (id != update.Id)
            return BadRequest();

        var existing = await _context.Skills.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.Name = update.Name;
        existing.Description = update.Description;
        existing.Category = update.Category;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/v1/skills/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var skill = await _context.Skills.FindAsync(id);
        if (skill == null)
            return NotFound();

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
