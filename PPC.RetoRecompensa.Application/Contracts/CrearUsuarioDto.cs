namespace PPC.RetoRecompensa.Application.Contracts;

public class CrearUsuarioDto : AccesoUsuarioDto
{
    public string? Nombre {get;set;}
    public string? Avatar {get;set;}
}