using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Estudiante")]
    public class EstudianteEntity
    {
        public EstudianteEntity()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            Clases = new List<ClaseEstudianteEntity>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }

        public bool IsDeleted { get; set; }
        public ICollection<ClaseEstudianteEntity> Clases { get; set; } 
    }
}
