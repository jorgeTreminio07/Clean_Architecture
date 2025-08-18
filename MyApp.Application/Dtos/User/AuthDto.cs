using MyApp.Application.Dtos.Rol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Dtos.User
{
    public class AuthDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public RolForAuthDto? Rol {  get; set; }
        public string? Avatar { get; set; }
        public string? Token { get; set; }

    }
}
