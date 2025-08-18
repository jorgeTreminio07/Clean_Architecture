using Ardalis.Result;
using MediatR;
using MyApp.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.User
{
    public record LoginUserCommand(string Email, string Password) : IRequest<Result<LoginDto>>
    {
    }
}
