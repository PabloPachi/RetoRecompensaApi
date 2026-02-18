using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Application.Interfaces;
using PPC.RetoRecompensa.Domain.Entities;
using PPC.RetoRecompensa.Domain.Exceptions;

namespace PPC.RetoRecompensa.Application.UseCases;

public class ObtenerRecompensaUseCase
{
    private readonly IUsuarioRepository _repo;
    private readonly IRecompensaRepository _reco;
    public ObtenerRecompensaUseCase(IUsuarioRepository repo, IRecompensaRepository reco)
    {
        _repo = repo;
        _reco = reco;
    }

    public async Task<RespuestaDto> Execute(ObtenerRecompensaDto solicitud)
    {
        Usuario usuario = await _repo.ObtenerUsuarioId(solicitud.IdUsuario) ?? throw new ValidationException("No existe el usuario");
        Recompensa recompensa = await _reco.ObtenerRecompensaId(solicitud.IdRecompensa) ?? throw new ValidationException("No existe la recompensa");
        if (recompensa.Puntos > usuario.Acumulado)
            throw new ValidationException("Puntaje acumulado del usuario es insuficiente");
        RecompensaUsuario? recompensaUsuario = await _reco.ObtenerRecompensaUsuario(solicitud.IdUsuario, solicitud.IdRecompensa);
        if (recompensaUsuario != null)
            throw new ValidationException("La recompensa ya fue obtenida por el usuario");
        usuario.Acumulado -= recompensa.Puntos;
        await _reco.InsertarRecompensaUsuario(solicitud.IdUsuario, solicitud.IdRecompensa);
        await _reco.GuardarAsync();
        RespuestaDto respuesta = new RespuestaDto();
        respuesta.EsCorrecto = true;
        respuesta.Mensaje = "Recompensa obtenida";
        return respuesta;
    }
}