using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using MyApp.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.User
{
    public record UpdateUserCommand(Guid Id, string Name, string Email, string Password, Guid RolId, IFormFile? Avatar) : IRequest<Result<AuthDto>>
    {
    }
}
