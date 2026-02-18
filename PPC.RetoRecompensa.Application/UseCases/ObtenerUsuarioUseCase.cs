using System.ComponentModel.DataAnnotations;
using System.Net.Cache;
using Microsoft.Extensions.Configuration;
using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Application.Interfaces;
using PPC.RetoRecompensa.Application.Mappers;
using PPC.RetoRecompensa.Domain.Entities;

namespace PPC.RetoRecompensa.Application.UseCases;

public class ObtenerUsuarioUseCase
{
    private readonly IUsuarioRepository _repo;
    private readonly IRecompensaRepository _reco;
    private readonly IRetoRepository _reto;
    private readonly bool _retoExtra;
    public ObtenerUsuarioUseCase(IUsuarioRepository repo, IRecompensaRepository reco, IRetoRepository reto, IConfiguration config)
    {
        _repo = repo;
        _reco = reco;
        _reto = reto;
        _retoExtra = Convert.ToBoolean(config["Features:EnableBonusChallenge"]);
    }

    public async Task<AccesoUsuarioRespuestaDto> Execute(ObtenerUsuarioDto solicitud)
    {
        if(_retoExtra)
            await _reto.AsignarRetoExtra(solicitud.IdUsuario);
        Usuario usuario = await _repo.ObtenerUsuarioCompletoId(solicitud.IdUsuario) ?? new Usuario();


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
        respuesta.Mensaje = "Usuario correcto";
        return respuesta;
    }
}