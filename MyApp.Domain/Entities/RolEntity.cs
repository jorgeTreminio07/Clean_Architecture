using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Roles")]
    public class RolEntity
    {
        public RolEntity()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            UserEntities = new List<UserEntity>();
            Permisos = new List<RolPermisoEntity>();
        }

        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }

        public ICollection<UserEntity> UserEntities { get; set; }

        public ICollection<RolPermisoEntity> Permisos { get; set; }
        public bool IsDeleted { get; set; }
    }
}
