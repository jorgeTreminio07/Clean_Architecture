using MyApp.Application.Dtos.User;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Dtos.Rol
{
    public class RolWithUsersDto
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        public List<UserWithoutRolDto>? UserEntities { get; set; }
        public List<string>? Permisos { get; set; }
    }
}
