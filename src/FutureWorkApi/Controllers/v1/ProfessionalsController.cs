using FutureWorkApi.Data;
using FutureWorkApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutureWorkApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProfessionalsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProfessionalsController(AppDbContext context)
    {
        _context = context;
    }

    // GET api/v1/professionals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Professional>>> GetAll()
    {
        var list = await _context.Professionals.AsNoTracking().ToListAsync();
        return Ok(list); // 200
    }

    // GET api/v1/professionals/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Professional>> GetById(int id)
    {
        var prof = await _context.Professionals.FindAsync(id);
        if (prof == null)
            return NotFound(); // 404

        return Ok(prof); // 200
    }

    // POST api/v1/professionals
    [HttpPost]
    public async Task<ActionResult<Professional>> Create(Professional professional)
    {
        _context.Professionals.Add(professional);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById),
            new { id = professional.Id, version = "1.0" }, professional); // 201
    }

    // PUT api/v1/professionals/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Professional update)
    {
        if (id != update.Id)
            return BadRequest(); // 400

        var existing = await _context.Professionals.FindAsync(id);
        if (existing == null)
            return NotFound(); // 404

        existing.FullName = update.FullName;
        existing.Email = update.Email;
        existing.FutureArea = update.FutureArea;

        await _context.SaveChangesAsync();
        return NoContent(); // 204
    }

    // DELETE api/v1/professionals/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var prof = await _context.Professionals.FindAsync(id);
        if (prof == null)
            return NotFound(); // 404

        _context.Professionals.Remove(prof);
        await _context.SaveChangesAsync();
        return NoContent(); // 204
    }
}
