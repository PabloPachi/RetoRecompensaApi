using Microsoft.EntityFrameworkCore;
using PPC.RetoRecompensa.Application.Interfaces;
using PPC.RetoRecompensa.Domain.Entities;

namespace PPC.RetoRecompensa.Infrastructure.Persistence.Repositories;

public class RetoRepository : IRetoRepository
{
    private readonly RetoRecompensaDbContext _context;
    public RetoRepository(RetoRecompensaDbContext context)
    {
        _context = context;
    }
    public async Task<bool> AsignarReto(int usuarioId, int retoId)
    {
        _context.RetoUsuario.Add(new RetoUsuario
        {
            IdUsuario = usuarioId,
            IdReto = retoId,
            Estado = false,
            Creacion = DateTime.Now
        });
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> AsignarRetoExtra(int usuarioId)
    {
        RetoUsuario? reto = await RetoAsignado(7, usuarioId);
        if (reto == null)
        {
            _context.RetoUsuario.Add(new RetoUsuario
            {
                IdUsuario = usuarioId,
                IdReto = 7,
                Estado = false,
                Creacion = DateTime.Now
            });
            await _context.SaveChangesAsync();
        }

        return true;
    }
    public async Task<List<int>> ObtenerRetosPorTipo(params string[] tipos)
    {
        return _context.Reto
                        .Where(r => tipos.Contains(r.Tipo))
                        .Select(r => r.Id)
                        .ToList();
    }
    public async Task<Reto?> ObtenerRetoId(int idReto)
    {
        return await _context.Reto.FirstOrDefaultAsync(u => u.Id == idReto);
    }
    public async Task<RetoUsuario?> RetoAsignado(int idReto, int idUsuario)
    {
        return await _context.RetoUsuario.FirstOrDefaultAsync(u => u.IdReto == idReto && u.IdUsuario == idUsuario);
    }
    public async Task GuardarAsync()
    {
        await _context.SaveChangesAsync();
    }
}
