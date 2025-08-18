using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Commands.Estudiante;
using MyApp.Application.Dtos.Estudiante;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Estudiante
{
    public class AddEstudianteCommandHandler : IRequestHandler<AddEstudianteCommand, Result<EstudianteDto>>
    {
        private readonly IAsyncRepository<EstudianteEntity> _estudianteRepository;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<ClaseEntity> _claseRepository;

        public AddEstudianteCommandHandler(IAsyncRepository<EstudianteEntity> estudianteRepository, IMapper mapper, IAsyncRepository<ClaseEntity> claseRepository)
        {
            this._estudianteRepository = estudianteRepository;
            this._mapper = mapper;
            this._claseRepository = claseRepository;
        }
        public async Task<Result<EstudianteDto>> Handle(AddEstudianteCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result.Invalid(new ValidationError("Request", "La solicitud es nula"));
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return Result.Invalid(new ValidationError("Name", "El nombre es obligatorio"));
            }

            var estudiante = _mapper.Map<EstudianteEntity>(request);
            estudiante.Clases = new List<ClaseEstudianteEntity>();

            if (request.clasesId != null && request.clasesId.Any())
            {
                var clases = await _claseRepository.ListAsync(cancellationToken);

                var clasesExist = clases.Where(c => request.clasesId.Contains(c.Id))
                    .ToList();

                foreach (var clase in clasesExist)
                {
                    clase.Estudiantes.Add(new ClaseEstudianteEntity
                        {
                            EstudianteId = estudiante.Id,
                            ClaseId = clase.Id,
                        }
                     );
                }
            }

            await _estudianteRepository.AddAsync(estudiante, cancellationToken);

            var estudianteDto = _mapper.Map<EstudianteDto>(estudiante);
            return Result.Success(estudianteDto);

        }
    }
}
