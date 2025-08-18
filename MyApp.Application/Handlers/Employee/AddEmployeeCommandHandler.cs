using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Commands.Employee;
using MyApp.Application.Dtos.Employee;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Employee
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand ,Result<EmployeeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<EmployeeEntity> _employeeRepository;
        private readonly IAsyncRepository<OfficeEntity> _officeRepository;

        public AddEmployeeCommandHandler(IMapper mapper, IAsyncRepository<EmployeeEntity> employeeRepository, IAsyncRepository<OfficeEntity> officeRepository)
        {
            this._mapper = mapper;
            this._employeeRepository = employeeRepository;
            this._officeRepository = officeRepository;
        }

        public async Task<Result<EmployeeDto>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result.Invalid(new ValidationError("Request", "El request no puede ser nulo"));
            }

            var office = await _officeRepository.GetByIdAsync(request.OfficeId);

            if (office == null)
            {
                return Result.Invalid(new ValidationError("Request", "El officeId no puede ser nulo"));
            }

            var employeeReq = _mapper.Map<EmployeeEntity>(request);
            employeeReq.Office = office;
            

            await _employeeRepository.AddAsync(employeeReq, cancellationToken);

            return Result.Success(_mapper.Map<EmployeeDto>(employeeReq));
        }
    }
}
