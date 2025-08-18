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
    public class AddOfficeCommandHandler : IRequestHandler<AddOfficeCommand, Result<OfficeDto>>
    {
        private readonly IAsyncRepository<OfficeEntity> _officeRepository;
        private readonly IMapper _mapper;

        public AddOfficeCommandHandler(IAsyncRepository<OfficeEntity> officeRepository, IMapper mapper)
        {
            this._officeRepository = officeRepository;
            this._mapper = mapper;
        }

        public async Task<Result<OfficeDto>> Handle(AddOfficeCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result.Invalid(new ValidationError("request", "Request null"));
            }

            var office = _mapper.Map<OfficeEntity>(request);

            await _officeRepository.AddAsync(office, cancellationToken);

            var officeDto = _mapper.Map<OfficeDto>(office);

            return Result.Success(officeDto);

        }
    }
}
