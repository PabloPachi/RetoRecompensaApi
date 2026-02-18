using System.Net.Cache;
using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Application.Interfaces;
using PPC.RetoRecompensa.Domain.Entities;

namespace PPC.RetoRecompensa.Application.Mappers;

public class BdMapper
{
    public static Usuario CrearUsuarioDtoToUsuario(CrearUsuarioDto dto)
    {
        return new Usuario()
        {
            Correo = dto.Correo,
            Pass = dto.Pass,
            Avatar = dto.Avatar,
            Nombre = dto.Nombre
        };
    }
    public static AccesoUsuarioRespuestaDto UsuarioToAccesoUsuarioRespuestaDto(Usuario user)
    {
        return new AccesoUsuarioRespuestaDto()
        {
            Id = user.Id,
            Correo = user.Correo,
            Avatar = user.Avatar,
            Nombre = user.Nombre,
            Puntos = user.Acumulado,
            RetosPendientes = (from r in user.Retos where r.Estado == false select new ItemInfoDto() { Id = r.Reto.Id, Nombre = r.Reto.Titulo, Descripcion = r.Reto.Descripcion }).ToList(),
            RetosCompletados = (from r in user.Retos where r.Estado == true select new ItemInfoDto() { Id = r.Reto.Id, Nombre = r.Reto.Titulo, Descripcion = r.Reto.Descripcion }).ToList(),
            RecompensasObtenidas = (from r in user.Recompensas select new ItemInfoDto() { Id = r.Recompensa.Id, Nombre = r.Recompensa.Nombre, Descripcion = r.Recompensa.Descripcion }).ToList()
        };
    }
}