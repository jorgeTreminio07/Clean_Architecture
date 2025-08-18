using Ardalis.Result;
using MediatR;
using MyApp.Application.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Employee
{
    public record AddEmployeeCommand(string Name, string Email, string Phone, Guid OfficeId) : IRequest<Result<EmployeeDto>>
    {
    }
}
