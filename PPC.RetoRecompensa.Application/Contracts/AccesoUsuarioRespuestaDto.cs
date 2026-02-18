namespace PPC.RetoRecompensa.Application.Contracts;

public class AccesoUsuarioRespuestaDto: RespuestaDto
{
    public int Id {get;set;}
    public string? Nombre {get;set;}
    public string? Avatar {get;set;}
    public string? Correo {get;set;}
    public int Puntos {get;set;}
    public List<ItemInfoDto>? RetosCompletados {get;set;}
    public List<ItemInfoDto>? RetosPendientes {get;set;}
    public List<ItemInfoDto>? RecompensasObtenidas {get;set;}
    public List<ItemInfoHabilitadoDto>? RecompensasDisponibles {get;set;}
}