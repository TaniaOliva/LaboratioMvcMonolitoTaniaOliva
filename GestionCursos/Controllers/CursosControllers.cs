using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCursos.Models;

namespace GestionCursos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly GestionCursosDbContext _context;

    public CursosController(GestionCursosDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cursos = await _context.Cursos.ToListAsync();
        return Ok(cursos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null)
            return NotFound($"No existe un curso con Id {id}.");

        return Ok(curso);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Curso curso)
    {
        if (string.IsNullOrWhiteSpace(curso.Nombre))
            return BadRequest("El nombre del curso es obligatorio.");

        if (curso.CreditosAcademicos < 1 || curso.CreditosAcademicos > 10)
            return BadRequest("Los créditos académicos deben estar entre 1 y 10.");

        var existeInstructor = await _context.Instructores.AnyAsync(i => i.Id == curso.InstructorId);
        if (!existeInstructor)
            return BadRequest($"No existe un instructor con Id {curso.InstructorId}.");

        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = curso.Id }, curso);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Curso curso)
    {
        if (id != curso.Id)
            return BadRequest("El Id de la ruta no coincide con el Id del cuerpo.");

        var cursoExistente = await _context.Cursos.FindAsync(id);
        if (cursoExistente == null)
            return NotFound($"No existe un curso con Id {id}.");

        if (string.IsNullOrWhiteSpace(curso.Nombre))
            return BadRequest("El nombre del curso es obligatorio.");

        if (curso.CreditosAcademicos < 1 || curso.CreditosAcademicos > 10)
            return BadRequest("Los créditos académicos deben estar entre 1 y 10.");

        var existeInstructor = await _context.Instructores.AnyAsync(i => i.Id == curso.InstructorId);
        if (!existeInstructor)
            return BadRequest($"No existe un instructor con Id {curso.InstructorId}.");

        cursoExistente.Nombre = curso.Nombre;
        cursoExistente.CreditosAcademicos = curso.CreditosAcademicos;
        cursoExistente.InstructorId = curso.InstructorId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null)
            return NotFound($"No existe un curso con Id {id}.");

        _context.Cursos.Remove(curso);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}