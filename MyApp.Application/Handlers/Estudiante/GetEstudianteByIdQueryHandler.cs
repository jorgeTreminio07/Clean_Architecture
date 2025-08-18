using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Dtos.Estudiante;
using MyApp.Application.Queries.Estudiante;
using MyApp.Application.Specifications.Estudiante;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Estudiante
{
    public class GetEstudianteByIdQueryHandler : IRequestHandler<GetEstudianteByIdQuery, Result<EstudianteDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<EstudianteEntity> _estudianteRepository;

        public GetEstudianteByIdQueryHandler(IMapper mapper, IAsyncRepository<EstudianteEntity> estudianteRepository)
        {
            this._mapper = mapper;
            this._estudianteRepository = estudianteRepository;
        }
        public async Task<Result<EstudianteDto>> Handle(GetEstudianteByIdQuery request, CancellationToken cancellationToken)
        {
            var estudiante = await _estudianteRepository.FirstOrDefaultAsync(new GetEstudianteByIdSpecification(request.Id) , cancellationToken);
            if (estudiante == null)
            {
                return Result.NotFound("no existe el estudiante");
            }

            var estudianteDto = _mapper.Map<EstudianteDto>(estudiante);

            return Result.Success(estudianteDto);
        }
    }
}
