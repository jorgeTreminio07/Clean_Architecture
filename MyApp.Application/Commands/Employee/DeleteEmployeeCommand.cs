using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Employee
{
    public record DeleteEmployeeCommand(Guid Id) : IRequest<Result<Guid>>
    {
    }
}
