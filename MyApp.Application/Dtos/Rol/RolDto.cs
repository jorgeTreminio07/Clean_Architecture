using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Dtos.Rol
{
    public class RolDto
    {
        public Guid? Id { get; set; }
       
        public string? Name { get; set; }
       
        public string? Description { get; set; }

        public List<string>? Permisos { get; set; }
    }
}
