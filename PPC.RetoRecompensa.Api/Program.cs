using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPC.RetoRecompensa.Api.Middleware;
using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Application.Interfaces;
using PPC.RetoRecompensa.Application.UseCases;
using PPC.RetoRecompensa.Application.Validators;
using PPC.RetoRecompensa.Infrastructure.Persistence;
using PPC.RetoRecompensa.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutterWeb",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:33639")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//builder.Services.AddValidatorsFromAssemblyContaining<CrearUsuarioDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CrearUsuarioDtoValidator>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(
                e => e.Key,
                e => e.Value.Errors.Select(v => v.ErrorMessage).ToArray()
            );
        var concatenatedMessages = string.Join("; ",
            errors.SelectMany(e => e.Value));
        var response = new RespuestaDto
        {
            EsCorrecto = false,
            Mensaje = $"La solicitud contiene errores: {concatenatedMessages}",
            //Detalle = errors
        };

        return new BadRequestObjectResult(response);
    };
});



builder.Services.AddDbContext<RetoRecompensaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RetoRecompensaConnection")));
builder.Services.AddScoped<RetoRecompensaDbContext>();
builder.Services.AddScoped<IRetoRepository, RetoRepository>();
builder.Services.AddScoped<IRecompensaRepository, RecompensaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<AccederUseCase>();
builder.Services.AddScoped<ObtenerUsuarioUseCase>();
builder.Services.AddScoped<CrearUsuarioUseCase>();
builder.Services.AddScoped<CompletarRetoUseCase>();
builder.Services.AddScoped<ObtenerRecompensaUseCase>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("AllowFlutterWeb");

app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

// Obtener el ensamblado de la aplicación actual
var assembly = Assembly.GetExecutingAssembly();
// Obtener la versión de compilado (generalmente AssemblyVersion o AssemblyInformationalVersion)
var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? assembly.GetName().Version?.ToString() ?? "Versión desconocida";
var responseContent = new
{
    Status = "Servicio RetoRecompensa activo",
    Environment = app.Environment.EnvironmentName,
    BuildVersion = version,
    HostName = Environment.MachineName
};
// Endpoint opcional para prueba
app.MapGet("/", () => Results.Ok(responseContent));

app.Run();

