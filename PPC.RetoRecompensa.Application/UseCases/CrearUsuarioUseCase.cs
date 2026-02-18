using System.Net.Cache;
using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Application.Interfaces;
using PPC.RetoRecompensa.Application.Mappers;
using PPC.RetoRecompensa.Domain.Exceptions;

namespace PPC.RetoRecompensa.Application.UseCases;

public class CrearUsuarioUseCase 
{
    private readonly IUsuarioRepository _repo;
    private readonly IRetoRepository _reto;
    public CrearUsuarioUseCase(IUsuarioRepository repo, IRetoRepository reto)
    {
        _repo = repo;
        _reto = reto;
    }
    public async Task<RespuestaDto> Execute(CrearUsuarioDto solicitud)
    {

        if (await _repo.ExisteUsuario(solicitud.Correo ?? ""))
        {
            throw new ValidationException("El usuario ya se encuentra registrado");
        }
        else
        {
            int idUsuario = await _repo.CrearUsuario(BdMapper.CrearUsuarioDtoToUsuario(solicitud));
            List<int> idsRetos = await _reto.ObtenerRetosPorTipo("simple", "doble");
            foreach (int idReto in idsRetos)
            {
                await _reto.AsignarReto(idUsuario, idReto);
            }
        }
        return new RespuestaDto() { EsCorrecto = true, Mensaje = "Usuario creado correctamente" };
    }
}