using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyApp.Application.Interface.Archivos;
using MyApp.Application.Interface.Security;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Arhivos;
using MyApp.Infrastructure.Backup;
using MyApp.Infrastructure.Persistence;
using MyApp.Infrastructure.Repository;
using MyApp.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {

            //Conexion Database
            var cs = configuration.GetConnectionString("Default");
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(cs));

            using (var serviceProvider = services.BuildServiceProvider())
            {
                try
                {
                    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.OpenConnection();
                    dbContext.Database.CloseConnection();
                    Console.WriteLine("Conexion a la base de datos exitosa");

                }catch (Exception ex)
                {
                    Console.WriteLine($"Error al conectar la base de datos: {ex.Message}");
                }
            }

            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));


            //Configuracion JWT
            var secret = configuration["AppSettings:Secret"] ?? "mysuperlongsecretkeywithmorethan32chars!";
            var issuer = configuration["AppSettings:JwtIssuer"] ?? "*";
            var audience = configuration["AppSettings:JwtAudience"] ?? "*";

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };
            });

            services.AddSingleton<IJwtService>(new JwtService(secret, issuer, audience));

            //Bcrypt HashPasswordService
            services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

            //Rol-Permisos
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            //Almacenar ArchivosService
            services.AddScoped<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
            services.AddHttpContextAccessor();

            //backup service
            services.AddScoped<IPostgresBackupService, PostgresBackupService>();

            return services;
        }
    }
}
