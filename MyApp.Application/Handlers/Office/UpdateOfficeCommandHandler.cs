using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Commands.Office;
using MyApp.Application.Dtos.Office;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Office
{
    public class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, Result<OfficeDto>>
    {
        private readonly IAsyncRepository<OfficeEntity> _officeRepository;
        private readonly IMapper _mapper;

        public UpdateOfficeCommandHandler(IAsyncRepository<OfficeEntity> officeRepository, IMapper mapper)
        {
            this._officeRepository = officeRepository;
            this._mapper = mapper;
        }
        public async Task<Result<OfficeDto>> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                return Result.Invalid(new ValidationError("request", "Request is null"));
            }

            var office = await _officeRepository.GetByIdAsync(request.Id, cancellationToken);

            if(office== null)
            {
                return Result.NotFound("no existe office");
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                office.Name = request.Name;
            }


            await _officeRepository.UpdateAsync(office, cancellationToken);

            var officeDto = _mapper.Map<OfficeDto>(office);

            return Result.Success(officeDto);
        }
    }
}
