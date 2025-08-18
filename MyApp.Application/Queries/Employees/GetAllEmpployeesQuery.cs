using Ardalis.Result;
using MediatR;
using MyApp.Application.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Queries.Employees
{
    public record GetAllEmpployeesQuery : IRequest<Result<List<EmployeeDto>>>
    {
    }
}
