namespace PPC.RetoRecompensa.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string? Correo { get; set; }
    public string? Pass { get; set; }
    public string? Nombre { get; set; }
    public string? Avatar { get; set; }
    public int Acumulado { get; set; }

    public ICollection<RetoUsuario> Retos { get; set; } = new List<RetoUsuario>();
    public ICollection<RecompensaUsuario> Recompensas { get; set; } = new List<RecompensaUsuario>();
}