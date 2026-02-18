using Microsoft.AspNetCore.Mvc;
using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Application.UseCases;


namespace PPC.RetoRecompensa.Api.Controllers;

[ApiController]
[Route("api/Recompensa")]
public class RecompensaController : ControllerBase
{
    private readonly ObtenerRecompensaUseCase _obtener;
    public RecompensaController(ObtenerRecompensaUseCase obtener)
    {
        _obtener = obtener;
    }
    
    [HttpPost("Obtener")]
    public async Task<IActionResult> ObtenerRecompensa(ObtenerRecompensaDto solicitud)
    {
        return Ok(await _obtener.Execute(solicitud));
    }
}