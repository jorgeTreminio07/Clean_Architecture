using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Dtos.Employee;
using MyApp.Application.Queries.Employees;
using MyApp.Application.Specifications.Employees;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Employee
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeDto>>
    {
        private readonly IAsyncRepository<EmployeeEntity> _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdHandler(IAsyncRepository<EmployeeEntity> employeeRepository, IMapper mapper)
        {
            this._employeeRepository = employeeRepository;
            this._mapper = mapper;
        }
        public async Task<Result<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync(new GetEmployeeByIdSpecification(request.Id), cancellationToken);

            if(employee == null)
            {
                return Result.NotFound();
            }

            if(employee.Office is not null)
            {
                if (employee.Office.IsDeleted)
                {
                    employee.Office = null;
                }
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Result.Success(employeeDto);
        }
    }
}
