namespace PPC.RetoRecompensa.Domain.Entities;

public class RecompensaUsuario
{
    public int Id { get; set; }
    public int IdRecompensa { get; set; }
    public int IdUsuario { get; set; }
    public DateTime Registro { get; set; }

    public Usuario Usuario { get; set; } = null!;
    public Recompensa Recompensa { get; set; } = null!;
}
