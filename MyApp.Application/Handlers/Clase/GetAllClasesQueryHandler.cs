using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Dtos.Clase;
using MyApp.Application.Queries.Clase;
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
    public class GetAllClasesQueryHandler : IRequestHandler<GetAllClasesQuery, Result<List<ClaseDto>>>
    {
        private readonly IAsyncRepository<ClaseEntity> _claseRepository;
        private readonly IMapper _mapper;

        public GetAllClasesQueryHandler(IAsyncRepository<ClaseEntity> claseRepository, IMapper mapper)
        {
            this._claseRepository = claseRepository;
            this._mapper = mapper;
        }

        public async Task<Result<List<ClaseDto>>> Handle(GetAllClasesQuery request, CancellationToken cancellationToken)
        {
            var clase = await _claseRepository.ListAsync(new ClaseSpecificationsWithEstudiantes(), cancellationToken);

            if (clase is null || clase.Count == 0)
            {
                return Result.NotFound();
            }

            var claseDto = _mapper.Map<List<ClaseDto>>(clase);
            return Result.Success(claseDto);
        }
    }
}
