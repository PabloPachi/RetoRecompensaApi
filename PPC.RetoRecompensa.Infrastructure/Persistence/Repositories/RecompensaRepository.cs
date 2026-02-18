using Microsoft.EntityFrameworkCore;
using PPC.RetoRecompensa.Application.Interfaces;
using PPC.RetoRecompensa.Domain.Entities;

namespace PPC.RetoRecompensa.Infrastructure.Persistence.Repositories;

public class RecompensaRepository : IRecompensaRepository
{
    private readonly RetoRecompensaDbContext _context;
    public RecompensaRepository(RetoRecompensaDbContext context)
    {
        _context = context;
    }

    public async Task GuardarAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Recompensa?> ObtenerRecompensaId(int idRecompensa)
    {
        return await _context.Recompensa.FirstOrDefaultAsync(u => u.Id == idRecompensa);
    }

    public async Task<RecompensaUsuario?> ObtenerRecompensaUsuario(int idUsuario, int idRecompensa)
    {
        return await _context.RecompensaUsuario.FirstOrDefaultAsync(u => u.Id == idRecompensa && u.IdUsuario == idUsuario);
    }
    public async Task InsertarRecompensaUsuario(int idUsuario, int idRecompensa)
    {
        await _context.RecompensaUsuario.AddAsync(new RecompensaUsuario()
        {
            IdRecompensa = idRecompensa,
            IdUsuario = idUsuario,
            Registro = DateTime.Now
        });
    }
    public async Task<List<Recompensa>?> ObtenerRecompensasDisponibles(List<int> recompensasObtenidas)
    {
        return await _context.Recompensa.Where(r => !recompensasObtenidas.Contains(r.Id)).ToListAsync();
    }
}
