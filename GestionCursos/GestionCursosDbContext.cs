using Microsoft.EntityFrameworkCore;
using GestionCursos.Models;

namespace GestionCursos;

public class GestionCursosDbContext : DbContext
{
    public GestionCursosDbContext(DbContextOptions<GestionCursosDbContext> options) : base(options)
    {
    }

    public DbSet<Instructor> Instructores { get; set; } = null!;
    public DbSet<Curso> Cursos { get; set; } = null!;
}