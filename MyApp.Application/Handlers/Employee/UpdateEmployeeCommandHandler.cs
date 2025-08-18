using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Commands.Employee;
using MyApp.Application.Dtos.Employee;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Employee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result<EmployeeDto>>
    {
        private readonly IAsyncRepository<EmployeeEntity> _employeeRespository;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<OfficeEntity> _officeRepository;

        public UpdateEmployeeCommandHandler(IAsyncRepository<EmployeeEntity> employeeRespository, IMapper mapper, IAsyncRepository<OfficeEntity> officeRepository)
        {
            this._employeeRespository = employeeRespository;
            this._mapper = mapper;
            this._officeRepository = officeRepository;
        }

        public async Task<Result<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            
                var employeeReq = _mapper.Map<EmployeeEntity>(request);

                var employeeExist = await _employeeRespository.GetByIdAsync(employeeReq.Id);

                if (employeeExist == null)
                {
                    return Result.Invalid(new ValidationError("Employee not exist"));
                }

                var office = await _officeRepository.GetByIdAsync(employeeReq.OfficeId);

                if (office == null)
                {
                    return Result.Invalid(new ValidationError("OfficeId not exist"));
                }

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    employeeExist.Name = request.Name;
                }

                if (!string.IsNullOrWhiteSpace(request.Email))
                {
                    employeeExist.Email = request.Email;
                }

                if (!string.IsNullOrWhiteSpace(request.Phone))
                {
                    employeeExist.Phone = request.Phone;
                }

                if (request.OfficeId != null && request.OfficeId != Guid.Empty)
                {
                    employeeExist.Office = office;
                }


                var updatedEmployee = await _employeeRespository.UpdateAsync(employeeExist, cancellationToken);
                

                return Result.Success(_mapper.Map<EmployeeDto>(employeeExist));
            
        }
    }
}
