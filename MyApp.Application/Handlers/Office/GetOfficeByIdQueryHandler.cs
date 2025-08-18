using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Dtos.Office;
using MyApp.Application.Queries.Offices;
using MyApp.Application.Specifications.Office;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Office
{
    public class GetOfficeByIdQueryHandler : IRequestHandler<GetOfficeByIdQuery, Result<OfficeDto>>
    {
        private readonly IAsyncRepository<OfficeEntity> _officeRepository;
        private readonly IMapper _mapper;

        public GetOfficeByIdQueryHandler(IAsyncRepository<OfficeEntity> officeRepository, IMapper mapper)
        {
            this._officeRepository = officeRepository;
            this._mapper = mapper;
        }
        public async Task<Result<OfficeDto>> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
        {
            var office = await _officeRepository.FirstOrDefaultAsync(new GetOfficeByIdSpecification(request.Id), cancellationToken);
            if (office == null)
            {
                return Result.NotFound();
            }

            var officeDto = _mapper.Map<OfficeDto>(office);
            return Result.Success(officeDto);
        }
    }
}
