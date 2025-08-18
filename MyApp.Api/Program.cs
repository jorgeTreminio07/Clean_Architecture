using Microsoft.Extensions.Options;
using MyApp.Api;
using MyApp.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuracion = builder.Configuration;

//cache
builder.Services.AddOutputCache(options =>
{
    options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(30); // 15 segundos
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//deendency injectionApi
builder.Services.AddAppDI(configuracion);

builder.Services.AddSwaggerGen();


builder.Services.AddAuthorization(opciones =>
{
    opciones.AddPolicy("PostOffice", policy =>
        policy.Requirements.Add(new PermissionRequirement("PostOffice")));
    opciones.AddPolicy("DeleteOffice", policy =>
        policy.Requirements.Add(new PermissionRequirement("DeleteOffice")));
    opciones.AddPolicy("GetOffice", policy =>
        policy.Requirements.Add(new PermissionRequirement("GetOffice")));
    opciones.AddPolicy("UpdateOffice", policy =>
        policy.Requirements.Add(new PermissionRequirement("UpdateOffice")));
});

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
app.UseSwaggerUI();

//para hacer login
app.UseAuthentication();
app.UseAuthorization();
//guardar imagenes
app.UseStaticFiles();

app.MapControllers();
app.UseCors("new"); 
app.Run();
