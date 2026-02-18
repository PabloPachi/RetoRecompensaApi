using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Application.Interfaces;
using PPC.RetoRecompensa.Application.Mappers;
using PPC.RetoRecompensa.Domain.Entities;
using PPC.RetoRecompensa.Domain.Exceptions;

namespace PPC.RetoRecompensa.Application.UseCases;

public class AccederUseCase
{
    private readonly IUsuarioRepository _repo;
    private readonly IRecompensaRepository _reco;
    public AccederUseCase(IUsuarioRepository repo, IRecompensaRepository reco)
    {
        _repo = repo;
        _reco = reco;
    }

    public async Task<AccesoUsuarioRespuestaDto> Execute(AccesoUsuarioDto solicitud)
    {
        Usuario usuario = await _repo.ObtenerUsuario(solicitud.Correo ?? "", solicitud.Pass ?? "") ?? new Usuario();
        if (usuario == null || usuario.Id == 0)
            throw new ValidationException("Usuario o contraseña incorrectos");
        AccesoUsuarioRespuestaDto respuesta = BdMapper.UsuarioToAccesoUsuarioRespuestaDto(usuario);
        List<Recompensa> recompensas = await _reco.ObtenerRecompensasDisponibles((from r in respuesta.RecompensasObtenidas select r.Id).ToList<int>()) ?? new List<Recompensa>();
        respuesta.RecompensasDisponibles = new List<ItemInfoHabilitadoDto>();
        foreach (Recompensa recompensa in recompensas)
        {
            respuesta.RecompensasDisponibles.Add(new ItemInfoHabilitadoDto()
            {
                Id = recompensa.Id,
                Nombre = recompensa.Nombre,
                Descripcion = recompensa.Descripcion,
                Habilitado = usuario.Acumulado >= recompensa.Puntos
            });
        }
        respuesta.EsCorrecto = true;
        respuesta.Mensaje = "Acceso correcto";
        return respuesta;
    }
}