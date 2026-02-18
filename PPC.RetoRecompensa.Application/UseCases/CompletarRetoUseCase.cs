using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Application.Interfaces;
using PPC.RetoRecompensa.Domain.Entities;
using PPC.RetoRecompensa.Domain.Exceptions;

namespace PPC.RetoRecompensa.Application.UseCases;

public class CompletarRetoUseCase
{
    private readonly IUsuarioRepository _repo;
    private readonly IRetoRepository _reto;
    public CompletarRetoUseCase(IUsuarioRepository repo, IRetoRepository reto)
    {
        _repo = repo;
        _reto = reto;
    }

    public async Task<RespuestaDto> Execute(CompletarRetoDto solicitud)
    {
        Usuario usuario = await _repo.ObtenerUsuarioId(solicitud.IdUsuario) ?? throw new ValidationException("No existe el usuario");
        Reto reto = await _reto.ObtenerRetoId(solicitud.IdReto) ?? throw new ValidationException("No existe el reto");
        RetoUsuario retoUsuario = await _reto.RetoAsignado(solicitud.IdReto, solicitud.IdUsuario) ?? throw new ValidationException("El reto no se encuentra asignado al usuario");
        if (retoUsuario.Estado)
            throw new ValidationException("El reto ya fue completado anteriormente");
        usuario.Acumulado += reto.Puntos;
        retoUsuario.Estado = true;
        retoUsuario.Completado = DateTime.Now;
        await _reto.GuardarAsync();
        RespuestaDto respuesta = new RespuestaDto();
        respuesta.EsCorrecto = true;
        respuesta.Mensaje = "Reto completado";
        return respuesta;
    }
}