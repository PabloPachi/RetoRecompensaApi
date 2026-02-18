using Microsoft.EntityFrameworkCore;
using PPC.RetoRecompensa.Application.Interfaces;
using PPC.RetoRecompensa.Domain.Entities;

namespace PPC.RetoRecompensa.Infrastructure.Persistence.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly RetoRecompensaDbContext _context;
    public UsuarioRepository(RetoRecompensaDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExisteUsuario(string correo)
    {
        return await _context.Usuario.AnyAsync(u => u.Correo == correo);
    }
    public async Task<Usuario?> ObtenerUsuarioId(int idUsuario)
    {
        return await _context.Usuario.FirstOrDefaultAsync(u => u.Id == idUsuario);
    }
    public async Task<Usuario?> ObtenerUsuarioCompletoId(int idUsuario)
    {
         return await _context.Usuario
        .Include(u => u.Retos)
        .ThenInclude(rp => rp.Reto)
        .Include(u => u.Recompensas)
        .ThenInclude(rc => rc.Recompensa)
        .FirstOrDefaultAsync(u => u.Id == idUsuario);
    }
    public async Task<int> CrearUsuario(Usuario usuario)
    {
        _context.Usuario.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario.Id;
    }

    public async Task<Usuario?> ObtenerUsuario(string correo, string Pass)
    {
        //return await _context.Usuario.FirstOrDefaultAsync(u => u.Correo == correo && u.Pass == Pass);
        return await _context.Usuario
        .Include(u => u.Retos)
        .ThenInclude(rp => rp.Reto)
        .Include(u => u.Recompensas)
        .ThenInclude(rc => rc.Recompensa)
        .FirstOrDefaultAsync(u => u.Correo == correo && u.Pass == Pass);
    }
}
