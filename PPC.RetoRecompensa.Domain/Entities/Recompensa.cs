namespace PPC.RetoRecompensa.Domain.Entities;

public class Recompensa
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public int Puntos { get; set; }

    public ICollection<RecompensaUsuario> Usuarios { get; set; } = new List<RecompensaUsuario>();
}
