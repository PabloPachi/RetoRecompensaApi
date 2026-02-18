using Microsoft.AspNetCore.Mvc;
using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Application.UseCases;


namespace PPC.RetoRecompensa.Api.Controllers;

[ApiController]
[Route("api/Usuario")]
public class UsuarioController : ControllerBase
{
    private readonly CrearUsuarioUseCase _crear;
    private readonly AccederUseCase _acceder;
    private readonly ObtenerUsuarioUseCase _obtener;
    public UsuarioController(CrearUsuarioUseCase crear, AccederUseCase acceder, ObtenerUsuarioUseCase obtener)
    {
        _crear = crear;
        _acceder = acceder;
        _obtener = obtener;
    }
    
    [HttpPost("CrearUsuario")]
    public async Task<IActionResult> CrearUsuario(CrearUsuarioDto solicitud)
    {
        return Ok(await _crear.Execute(solicitud));
    }
    [HttpPost("Acceder")]
    public async Task<IActionResult> Acceder(AccesoUsuarioDto solicitud)
    {
        return Ok(await _acceder.Execute(solicitud));
    }
    [HttpPost("Obtener")]
    public async Task<IActionResult> Obtener(ObtenerUsuarioDto solicitud)
    {
        return Ok(await _obtener.Execute(solicitud));
    }
}