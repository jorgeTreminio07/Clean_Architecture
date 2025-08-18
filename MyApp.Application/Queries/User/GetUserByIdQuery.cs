using Ardalis.Result;
using MediatR;
using MyApp.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Queries.User
{
    public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto>>
    {
    }
}
