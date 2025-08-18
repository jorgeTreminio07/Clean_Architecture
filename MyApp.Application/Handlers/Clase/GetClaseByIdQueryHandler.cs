using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Dtos.Clase;
using MyApp.Application.Queries.Clase;
using MyApp.Application.Queries.Offices;
using MyApp.Application.Specifications.Clase;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Clase
{
    public class GetClaseByIdQueryHandler : IRequestHandler<GetClaseByIdQuery, Result<ClaseDto>>
    {
        private readonly IAsyncRepository<ClaseEntity> _claseRepository;
        private readonly IMapper _mapper;

        public GetClaseByIdQueryHandler(IAsyncRepository<ClaseEntity> claseRepository, IMapper mapper)
        {
            this._claseRepository = claseRepository;
            this._mapper = mapper;
        }

        public async Task<Result<ClaseDto>> Handle(GetClaseByIdQuery request, CancellationToken cancellationToken)
        {
            var clase = await _claseRepository.FirstOrDefaultAsync(new GetClaseByIdSpecification(request.Id) , cancellationToken);

            if(clase == null)
            {
                return Result.NotFound("no existe la clase");
            }

            var claseDto = _mapper.Map<ClaseDto>(clase);

            return Result.Success(claseDto);
        }
    }
}
