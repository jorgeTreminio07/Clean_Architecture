using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyApp.Domain.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Backup
{
    public class PostgresBackupService : IPostgresBackupService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        

        public PostgresBackupService(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        public async Task<string> GenerarBackupAsync(string contenedor)
        {
            // Crear la carpeta si no existe
            var folder = Path.Combine(_env.WebRootPath, contenedor);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            // Nombre del archivo .dump
            var nombreArchivo = $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.dump";
            var rutaArchivo = Path.Combine(folder, nombreArchivo);

            // Obtener los parámetros de conexión
            var connectionString = _configuration.GetConnectionString("Default");
            var builder = new NpgsqlConnectionStringBuilder(connectionString);

            var usuario = builder.Username;
            var host = builder.Host;
            var puerto = builder.Port;
            var dbname = builder.Database;
            var password = builder.Password;

            // Verificar si pg_dump está disponible
            string pgDumpPath = "pg_dump"; // Puedes poner la ruta absoluta si es necesario

            var argumentos = $"-h {host} -p {puerto} -U {usuario} -F c -b -v -f \"{rutaArchivo}\" {dbname}";

            var startInfo = new ProcessStartInfo
            {
                FileName = pgDumpPath,
                Arguments = argumentos,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            startInfo.EnvironmentVariables["PGPASSWORD"] = password;

            using var proceso = new Process { StartInfo = startInfo };

            try
            {
                proceso.Start();

                string salida = await proceso.StandardOutput.ReadToEndAsync();
                string error = await proceso.StandardError.ReadToEndAsync();

                await proceso.WaitForExitAsync();

                if (proceso.ExitCode != 0)
                {
                    throw new Exception($"Error al generar el backup: {error}");
                }

                // URL pública del archivo

                var url = $"/{contenedor}/{nombreArchivo}";
                return url;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al intentar generar el respaldo de la base de datos.", ex);
            }
        }

        public async Task<string> RestaurarBackupAsync(string rutaRelativaArchivoDump)
        {
            var rutaArchivo = Path.Combine(_env.WebRootPath, rutaRelativaArchivoDump.TrimStart('/'));

            if (!File.Exists(rutaArchivo))
                throw new FileNotFoundException("No se encontró el archivo de backup para restaurar.", rutaArchivo);

            var connectionString = _configuration.GetConnectionString("Default");
            var builder = new NpgsqlConnectionStringBuilder(connectionString);

            var usuario = builder.Username;
            var host = builder.Host;
            var puerto = builder.Port;
            var dbname = builder.Database;
            var password = builder.Password;

            // Usa --clean y --if-exists para evitar bloqueos y borrar objetos si existen
            var argumentos = $"-h {host} -p {puerto} -U {usuario} -d {dbname} --clean --if-exists -v \"{rutaArchivo}\"";

            var startInfo = new ProcessStartInfo
            {
                FileName = "pg_restore",
                Arguments = argumentos,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            startInfo.EnvironmentVariables["PGPASSWORD"] = password;

            using var proceso = new Process { StartInfo = startInfo };

            try
            {
                proceso.Start();

                var outputTask = proceso.StandardOutput.ReadToEndAsync();
                var errorTask = proceso.StandardError.ReadToEndAsync();

                await Task.WhenAll(outputTask, errorTask);
                await proceso.WaitForExitAsync();

                if (proceso.ExitCode != 0)
                    throw new Exception($"Error al restaurar el backup: {errorTask.Result}");

                return rutaRelativaArchivoDump;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al intentar restaurar el respaldo de la base de datos.", ex);
            }
        }

        public string DeleteBackup(string url)
        {
            try
            {
                var rutaArchivo = Path.Combine(_env.WebRootPath, url.TrimStart('/'));

                if (!File.Exists(rutaArchivo))
                    throw new FileNotFoundException("El archivo de backup no existe.", rutaArchivo);

                File.Delete(rutaArchivo);

                return $"Archivo eliminado: {url}";
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al eliminar el archivo de backup.", ex);
            }
        }


    }
}
