using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Dtos.Rol
{
    public class RolForAuthDto
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
