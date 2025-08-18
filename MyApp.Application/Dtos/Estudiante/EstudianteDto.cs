using MyApp.Application.Dtos.Clase;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Dtos.Estudiante
{
    public class EstudianteDto
    {
        public Guid Id { get; set; }
        
        public string? Name { get; set; }
        public List<ClaseWithoutEstudiantesDto> Clases { get; set; } = [];
    }
}
