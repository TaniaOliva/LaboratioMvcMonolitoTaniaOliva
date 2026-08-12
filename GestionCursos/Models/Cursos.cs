namespace GestionCursos.Models;

public class Curso
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int CreditosAcademicos { get; set; }
    public int InstructorId { get; set; }
}