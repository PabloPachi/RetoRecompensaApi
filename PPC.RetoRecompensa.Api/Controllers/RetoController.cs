using Microsoft.AspNetCore.Mvc;
using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Application.UseCases;


namespace PPC.RetoRecompensa.Api.Controllers;

[ApiController]
[Route("api/Reto")]
public class RetoController : ControllerBase
{
    private readonly CompletarRetoUseCase _completar;
    public RetoController(CompletarRetoUseCase completar)
    {
        _completar = completar;
    }
    
    [HttpPost("Completar")]
    public async Task<IActionResult> CompletarReto(CompletarRetoDto solicitud)
    {
        return Ok(await _completar.Execute(solicitud));
    }
}