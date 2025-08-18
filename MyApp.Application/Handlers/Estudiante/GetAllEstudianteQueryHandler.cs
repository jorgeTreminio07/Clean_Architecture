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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Estudiante
{
    public class GetAllEstudianteQueryHandler : IRequestHandler<GetAllEstudianteQuery, Result<List<EstudianteDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<EstudianteEntity> _estudianteRepository;

        public GetAllEstudianteQueryHandler(IMapper mapper, IAsyncRepository<EstudianteEntity> estudianteRepository)
        {
            this._mapper = mapper;
            this._estudianteRepository = estudianteRepository;
        }
        public async Task<Result<List<EstudianteDto>>> Handle(GetAllEstudianteQuery request, CancellationToken cancellationToken)
        {
            var estudiantes = await _estudianteRepository.ListAsync(new EstudianteSpecificationWithClase(), cancellationToken);

            if (estudiantes is null || estudiantes.Count == 0)
            {
                return Result.NotFound();
            }

            var estudianteDto = _mapper.Map<List<EstudianteDto>>(estudiantes);

            return Result.Success(estudianteDto);
        }
    }
}
