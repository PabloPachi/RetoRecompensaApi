using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Domain.Entities;

namespace PPC.RetoRecompensa.Application.Interfaces;

public interface IUsuarioRepository
{
    public Task<bool> ExisteUsuario(string correo);
    public Task<Usuario?> ObtenerUsuarioId(int idUsuario);
    public Task<Usuario?> ObtenerUsuarioCompletoId(int idUsuario);
    public Task<int> CrearUsuario(Usuario usuario);
    public Task<Usuario?> ObtenerUsuario(string correo, string Pass);
}