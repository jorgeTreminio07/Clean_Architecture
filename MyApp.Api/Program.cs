using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MyApp.Api;
using MyApp.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuracion = builder.Configuration;

//cache
builder.Services.AddOutputCache(options =>
{
    options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(30); // 30 segundos
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//dependency injectionApi
builder.Services.AddAppDI(configuracion);

//Configuracion swagger
//Configuracion swagger
builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Plantilla Web API Clean Architecture ASP.NET Core 9",
        Description = @"Plantilla Web API aplicando Clean Architecture para desarrolladores que no quieren reinventar la rueda y agilizar el desarrollo de sus proyectos.

        **Características:**
        - Autenticación con JWT Bearer
        - Hash de contraseñas
        - CRUD de Usuarios
        - CRUD de Roles
        - Permisos de Roles
        - CRUD de Backups (archivos locales)
        - Manejo de Caché (puedes migrar a Redis)
        - Almacenamiento de Archivos local.

        - Endpoints de pruebas:
          -Relacion muchos a muchos: Estudiantes - Clases
          -Relacion uno a muchos: Employees - Offices.
          
        -Aplicacion de Patrones de diseño:
          - CQRS
          - Mediator
          - Repository
          - Specification
          - Dependency Injection.
          
        -Uso de librerias:
          -MediatR
          -AutoMapper
          -Ardalis.Result
          -Ardalis.Specification
          -Bcrypt.

        -Base de Datos PostgreSQL
        -Entity Framework Core
        -Fluent api.

        - Uso de CORS
        
        **Contacto:** 
        eduardotreminio10@gmail.com",

        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            // Email = "eduardotreminio10@gmail.com",
            // Name = "Jorge Eduardo Treminio Cruz",
        }
    });

    //Datos de la seguridad de la API para swagger
    opciones.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    opciones.OperationFilter<FiltroAutorizacion>();
});


//configuracion cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("new", app =>
    {
        app.AllowAnyOrigin();
        app.AllowAnyHeader();
        app.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(opciones => opciones.SwaggerEndpoint("/swagger/v1/swagger.json", "Biblioteca API V1"));

//para hacer login
app.UseAuthentication();
app.UseAuthorization();
//guardar imagenes
app.UseStaticFiles();

app.MapControllers();
app.UseCors("new");
app.Run();
