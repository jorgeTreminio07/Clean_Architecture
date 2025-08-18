using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("RolPermiso")]
    public class RolPermisoEntity
    {
        public Guid RolId { get; set; }
        public RolEntity? Rol { get; set; }

        public Guid PermisoId { get; set; }
        public PermisosEntity? Permisos { get; set; }
    }
}
