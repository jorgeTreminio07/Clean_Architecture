using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Permisos")]
    public class PermisosEntity
    {
        public PermisosEntity()
        {
            Id = Guid.NewGuid();
            Roles = new List<RolPermisoEntity>();

        }
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }

        public ICollection<RolPermisoEntity>? Roles { get; set; }
    }
}
