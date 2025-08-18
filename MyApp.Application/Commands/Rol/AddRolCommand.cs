using Ardalis.Result;
using MediatR;
using MyApp.Application.Dtos.Rol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Rol
{
    public record AddRolCommand(string Name, string Description, List<string>? NamesPermisos) : IRequest<Result<RolDto>>
    {
    }
}
