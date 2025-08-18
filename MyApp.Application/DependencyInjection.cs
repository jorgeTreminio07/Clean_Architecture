using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services, IConfiguration configuration)
        {
            //mediatR
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(op => op.RegisterServicesFromAssembly(assembly));

            //automapper
            services.AddAutoMapper(assembly);


            return services;
        }
    }
}
