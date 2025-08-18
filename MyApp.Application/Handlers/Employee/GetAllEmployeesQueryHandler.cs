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
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmpployeesQuery, Result<List<EmployeeDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<EmployeeEntity> _employeeRepository;

        public GetAllEmployeesQueryHandler(IMapper mapper, IAsyncRepository<EmployeeEntity> employeeRepository)
        {
            this._mapper = mapper;
            this._employeeRepository = employeeRepository;
        }

        public async Task<Result<List<EmployeeDto>>> Handle(GetAllEmpployeesQuery request, CancellationToken cancellationToken)
        {
            var getEmployee = await _employeeRepository.ListAsync(new IncludeEmployeeSpecifications(), cancellationToken);

            if(getEmployee == null)
            {
                return Result.NotFound();
            }

            foreach(var employees in getEmployee)
            {
                if (employees.Office is not null)
                {
                    if (employees.Office.IsDeleted)
                    {
                        employees.Office = null;
                    }
                }
            }

            var employeeDto = _mapper.Map<List<EmployeeDto>>(getEmployee);

            return Result.Success(employeeDto);
                
        }
    }
}
