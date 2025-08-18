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
    public class GetAllOfficeQueryHandler : IRequestHandler<GetAllOfficesQuery, Result<List<OfficeDtoWithEmployees>>>
    {
        private readonly IAsyncRepository<OfficeEntity> _officeRepository;
        private readonly IMapper _mapper;

        public GetAllOfficeQueryHandler(IAsyncRepository<OfficeEntity> officeRepository, IMapper mapper)
        {
            this._officeRepository = officeRepository;
            this._mapper = mapper;
        }

        public async Task<Result<List<OfficeDtoWithEmployees>>> Handle(GetAllOfficesQuery request, CancellationToken cancellationToken)
        {
            var offices = await _officeRepository.ListAsync(new IncludeEmployeesInOfficeSpecification(), cancellationToken);

            if (offices is null || offices.Count == 0)
            {
                return Result.NotFound();
            }

            var officeDto = _mapper.Map<List<OfficeDtoWithEmployees>>(offices);

            return Result.Success(officeDto);

        }
    }
}
