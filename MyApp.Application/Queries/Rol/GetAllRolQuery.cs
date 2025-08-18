using Ardalis.Result;
using MediatR;
using MyApp.Application.Dtos.Rol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Queries.Rol
{
    public record GetAllRolQuery : IRequest<Result<List<RolWithUsersDto>>>
    {
    }
}
