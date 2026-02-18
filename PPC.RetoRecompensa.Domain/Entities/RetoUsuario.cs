
namespace PPC.RetoRecompensa.Domain.Entities;

public class RetoUsuario
{
    public int Id { get; set; }
    public int IdReto { get; set; }
    public int IdUsuario { get; set; }
    public bool Estado { get; set; }
    public DateTime Creacion { get; set; }
    public DateTime? Completado { get; set; }

    public Usuario Usuario { get; set; } = null!;
    public Reto Reto { get; set; } = null!;
}
