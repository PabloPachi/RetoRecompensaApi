using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Domain.Entities;

namespace PPC.RetoRecompensa.Application.Interfaces;

public interface IRecompensaRepository
{
    public Task<Recompensa?> ObtenerRecompensaId(int idRecompensa);
    public Task<RecompensaUsuario?> ObtenerRecompensaUsuario(int idUsuario, int idRecompensa);
    public Task InsertarRecompensaUsuario(int idUsuario, int idRecompensa);
    public Task<List<Recompensa>?> ObtenerRecompensasDisponibles(List<int> recompensasObtenidas);

    public Task GuardarAsync();
}