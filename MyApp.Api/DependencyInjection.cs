using MyApp.Application;
using MyApp.Infrastructure;
using MyApp.Infrastructure.Security;

namespace MyApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI(configuration)
                .AddInfrastructureDI(configuration);

            //autenticacion
            services.AddAuthorization(opciones =>
            {
                opciones.AddPolicy("PostOffice", policy =>
                    policy.Requirements.Add(new PermissionRequirement("PostOffice")));
                opciones.AddPolicy("DeleteOffice", policy =>
                    policy.Requirements.Add(new PermissionRequirement("DeleteOffice")));
                opciones.AddPolicy("GetOffice", policy =>
                    policy.Requirements.Add(new PermissionRequirement("GetOffice")));
                opciones.AddPolicy("UpdateOffice", policy =>
                    policy.Requirements.Add(new PermissionRequirement("UpdateOffice")));
                opciones.AddPolicy("GetOfficeById", policy =>
                    policy.Requirements.Add(new PermissionRequirement("GetOfficeById")));

                opciones.AddPolicy("GetUserById", policy =>
                    policy.Requirements.Add(new PermissionRequirement("GetUserById")));
                opciones.AddPolicy("GetUser", policy =>
                    policy.Requirements.Add(new PermissionRequirement("GetUser")));
                opciones.AddPolicy("RegisterUser", policy =>
                    policy.Requirements.Add(new PermissionRequirement("RegisterUser")));
                opciones.AddPolicy("UpdateUser", policy =>
                    policy.Requirements.Add(new PermissionRequirement("UpdateUser")));
                opciones.AddPolicy("DeleteUser", policy =>
                    policy.Requirements.Add(new PermissionRequirement("DeleteUser")));

                opciones.AddPolicy("GetBackup", policy =>
                    policy.Requirements.Add(new PermissionRequirement("GetBackup")));
                opciones.AddPolicy("PostBackup", policy =>
                    policy.Requirements.Add(new PermissionRequirement("PostBackup")));
                opciones.AddPolicy("RestoreBackup", policy =>
                    policy.Requirements.Add(new PermissionRequirement("RestoreBackup")));
                opciones.AddPolicy("DeleteBackup", policy =>
                    policy.Requirements.Add(new PermissionRequirement("DeleteBackup")));

                opciones.AddPolicy("GetEmployeeById", policy =>
                    policy.Requirements.Add(new PermissionRequirement("GetEmployeeById")));
                opciones.AddPolicy("GetEmployees", policy =>
                    policy.Requirements.Add(new PermissionRequirement("GetEmployees")));
                opciones.AddPolicy("PostEmployee", policy =>
                    policy.Requirements.Add(new PermissionRequirement("PostEmployee")));
                opciones.AddPolicy("UpdateEmployee", policy =>
                    policy.Requirements.Add(new PermissionRequirement("UpdateEmployee")));
                opciones.AddPolicy("DeleteEmployee", policy =>
                    policy.Requirements.Add(new PermissionRequirement("DeleteEmployee")));

                opciones.AddPolicy("GetClaseById", policy =>
                    policy.Requirements.Add(new PermissionRequirement("GetClaseById")));
                opciones.AddPolicy("GetClase", policy =>
                    policy.Requirements.Add(new PermissionRequirement("GetClase")));
                opciones.AddPolicy("PostClase", policy =>
                    policy.Requirements.Add(new PermissionRequirement("PostClase")));
                opciones.AddPolicy("UpdateClase", policy =>
                    policy.Requirements.Add(new PermissionRequirement("UpdateClase")));
                opciones.AddPolicy("DeleteClase", policy =>
                    policy.Requirements.Add(new PermissionRequirement("DeleteClase")));

                opciones.AddPolicy("GetEstudianteById", policy =>
                    policy.Requirements.Add(new PermissionRequirement("GetEstudianteById")));
                opciones.AddPolicy("GetEstudiante", policy =>
                    policy.Requirements.Add(new PermissionRequirement("GetEstudiante")));
                opciones.AddPolicy("PostEstudiante", policy =>
                    policy.Requirements.Add(new PermissionRequirement("PostEstudiante")));
                opciones.AddPolicy("UpdateEstudiante", policy =>
                    policy.Requirements.Add(new PermissionRequirement("UpdateEstudiante")));
                opciones.AddPolicy("DeleteEstudiante", policy =>
                    policy.Requirements.Add(new PermissionRequirement("DeleteEstudiante")));

                opciones.AddPolicy("GetRol", policy =>
                    policy.Requirements.Add(new PermissionRequirement("GetRol")));
                opciones.AddPolicy("PostRol", policy =>
                    policy.Requirements.Add(new PermissionRequirement("PostRol")));
                opciones.AddPolicy("UpdateRol", policy =>
                    policy.Requirements.Add(new PermissionRequirement("UpdateRol")));
                opciones.AddPolicy("DeleteRol", policy =>
                    policy.Requirements.Add(new PermissionRequirement("DeleteRol")));
            });

            return services;
        }
    }
}
