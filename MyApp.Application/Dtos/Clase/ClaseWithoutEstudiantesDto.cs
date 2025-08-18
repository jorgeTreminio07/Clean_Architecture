using MyApp.Application.Dtos.Estudiante;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Dtos.Clase
{
    public class ClaseWithoutEstudiantesDto
    {
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
        //public List<EstudianteDto> Estudiantes { get; set; } = [];
    }
}
