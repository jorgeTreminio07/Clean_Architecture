using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("ClaseEntityEstudianteEntity")]
    public class ClaseEstudianteEntity
    {
        public Guid ClaseId { get; set; }
        public ClaseEntity? Clases { get; set; }

        public Guid EstudianteId { get; set; }
        public EstudianteEntity? Estudiantes { get; set; }
    }
}
