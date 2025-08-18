using Ardalis.Result;
using MediatR;
using MyApp.Application.Commands.Employee;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Employee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result<Guid>>
    {
        private readonly IAsyncRepository<EmployeeEntity> _employeeRepository;

        public DeleteEmployeeCommandHandler(IAsyncRepository<EmployeeEntity> employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
        public async Task<Result<Guid>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);

            if (employee == null)
            {
                return Result.NotFound("Id no existe");
            }

            employee.IsDeleted = true;

            await _employeeRepository.UpdateAsync(employee);

            return Result.Success(employee.Id);
        }
    }
}
