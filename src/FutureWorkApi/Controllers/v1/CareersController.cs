using FutureWorkApi.Data;
using FutureWorkApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutureWorkApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CareersController : ControllerBase
{
    private readonly AppDbContext _context;

    public CareersController(AppDbContext context)
    {
        _context = context;
    }

    // GET api/v1/careers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Career>>> GetAll()
    {
        var list = await _context.Careers.AsNoTracking().ToListAsync();
        return Ok(list);
    }

    // GET api/v1/careers/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Career>> GetById(int id)
    {
        var career = await _context.Careers.FindAsync(id);
        if (career == null)
            return NotFound();

        return Ok(career);
    }

    // POST api/v1/careers
    [HttpPost]
    public async Task<ActionResult<Career>> Create(Career career)
    {
        _context.Careers.Add(career);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById),
            new { id = career.Id, version = "1.0" }, career);
    }

    // PUT api/v1/careers/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Career update)
    {
        if (id != update.Id)
            return BadRequest();

        var existing = await _context.Careers.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.Name = update.Name;
        existing.Description = update.Description;
        existing.FutureDemandLevel = update.FutureDemandLevel;
        existing.RequiredSkillsOverview = update.RequiredSkillsOverview;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/v1/careers/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var career = await _context.Careers.FindAsync(id);
        if (career == null)
            return NotFound();

        _context.Careers.Remove(career);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
