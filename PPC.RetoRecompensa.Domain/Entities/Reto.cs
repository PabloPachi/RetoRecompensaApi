namespace PPC.RetoRecompensa.Domain.Entities;

public class Reto
{
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public string? Descripcion { get; set; }
    public int Puntos { get; set; }
    public string? Tipo { get; set; }

    public ICollection<RetoUsuario> Usuarios { get; set; } = new List<RetoUsuario>();
}