using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Clase")]
    public class ClaseEntity
    {
        public ClaseEntity()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            Estudiantes = new List<ClaseEstudianteEntity>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }

        public bool IsDeleted { get; set; }
        public ICollection<ClaseEstudianteEntity> Estudiantes { get; set; }
    }
}
