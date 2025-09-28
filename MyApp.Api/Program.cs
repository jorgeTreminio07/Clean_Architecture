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
        Title = "Plantilla Clean Architecture",
        Description = "Plantilla Web API aplicando Clean Architecture para desarrolladores que no quieren reinventar la rueda y agilizar el desarrollo de sus proyectos, La API Posee Autenticacion con JWT Bearer, Crud Usuarios, Crud Roles, Permisos de Roles, Crud Bakups(Se guardan en archivos local), Manejo de Caché (puedes migrar a redis), Almacenamiento de Archivos de forma local.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "eduardotreminio10@gmail.com",
            Name = "Jorge Eduardo Treminio Cruz",
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
