using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCursos.Models;

namespace GestionCursos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstructoresController : ControllerBase
{
    private readonly GestionCursosDbContext _context;

    public InstructoresController(GestionCursosDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var instructores = await _context.Instructores.ToListAsync();
        return Ok(instructores);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Instructor instructor)
    {
        if (string.IsNullOrWhiteSpace(instructor.Nombre))
            return BadRequest("El nombre del instructor es obligatorio.");

        _context.Instructores.Add(instructor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = instructor.Id }, instructor);
    }
}