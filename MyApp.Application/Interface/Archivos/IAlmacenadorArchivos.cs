using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interface.Archivos
{
    public interface IAlmacenadorArchivos
    {
        Task Borrar(string? ruta, string contenedor);
        Task<string> Almacenar(string contenedor, IFormFile archivo);
        Task<string> Editar(string? ruta, string contenedor, IFormFile archivo);
    }
}
