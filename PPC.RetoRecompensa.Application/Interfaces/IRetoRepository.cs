using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Domain.Entities;

namespace PPC.RetoRecompensa.Application.Interfaces;

public interface IRetoRepository
{
    public Task<List<int>> ObtenerRetosPorTipo(params string[] tipos);
    public Task<bool> AsignarReto(int idUsuario, int idReto);
    public Task<bool> AsignarRetoExtra(int idUsuario);
    public Task<Reto?> ObtenerRetoId(int idReto);
    public Task<RetoUsuario?> RetoAsignado(int idReto, int idUsuario);
    public Task GuardarAsync();
}