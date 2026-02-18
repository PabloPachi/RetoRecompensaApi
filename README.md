# RetoRecompensaApi
Prueba RetoRecompensa para Banco Sol
se trabajó en .Net 8.0

Pasos para levantar BD
========================
ejecutar los scripts en orden:
estructura.sql
datos iniciales.sql

• Cómo correr backend
=====================
dentro del proyecto API buscar Program.cs
y configurar cors para aceptar frontend (WithOrigins)
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


• Cómo activar/desactivar bonus challenge
=========================================
dentro del proyecto API buscar appsettings.js
y cambiar el valor de:
    "EnableBonusChallenge": true
