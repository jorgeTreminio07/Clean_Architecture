using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Clase
{
    public record DeleteClaseCommand (Guid Id) : IRequest<Result<Guid>>
    {
    }
}
