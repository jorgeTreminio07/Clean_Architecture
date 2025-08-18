using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Commands.Estudiante;
using MyApp.Application.Dtos.Estudiante;
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
    public class UpdateEstudianteCommandHandler : IRequestHandler<UpdateEstudianteComand, Result<EstudianteDto>>
    {
        private readonly IAsyncRepository<EstudianteEntity> _estudianteRepository;
        private readonly IAsyncRepository<ClaseEntity> _claseRepository;
        private readonly IAsyncRepository<ClaseEstudianteEntity> _claseEstudianteRepository;
        private readonly IMapper _mapper;

        public UpdateEstudianteCommandHandler(IAsyncRepository<EstudianteEntity> estudianteRepository, IAsyncRepository<ClaseEntity> claseRepository, IAsyncRepository<ClaseEstudianteEntity> claseEstudianteRepository, IMapper mapper)
        {
            this._estudianteRepository = estudianteRepository;
            this._claseRepository = claseRepository;
            this._claseEstudianteRepository = claseEstudianteRepository;
            this._mapper = mapper;
        }

        public async Task<Result<EstudianteDto>> Handle(UpdateEstudianteComand request, CancellationToken cancellationToken)
        {
            var estudiante = await _estudianteRepository.GetByIdAsync(request.Id, cancellationToken);
            if(estudiante == null)
            {
                return Result.NotFound($"No se encontró el estudiante con ID: {request.Id}");

            }

            estudiante.Name = request.Name;

            var relacionesExistentes = await _claseEstudianteRepository.ListAsync(new GetByIdEspecifications(request.Id), cancellationToken);
            var idClasesActuales = relacionesExistentes.Select(e => e.ClaseId).ToHashSet();
            var idNuevos = (request.ClasesId ?? new List<Guid>()).ToHashSet();

            var relacionesAEliminar = relacionesExistentes
                .Where(c => !idNuevos.Contains(c.ClaseId))
                .ToList();

            if (relacionesAEliminar.Any())
            {
                await _claseEstudianteRepository.DeleteRangeAsync(relacionesAEliminar, cancellationToken);
            }


            var idsAgregar = idNuevos.Where(id => !idClasesActuales.Contains(id)).ToList();



            foreach(var id in idsAgregar)
            {
                var clase = await _claseRepository.GetByIdAsync(id);

                if (clase != null)
                {
                    var nuevaRelacion = new ClaseEstudianteEntity
                    {
                        EstudianteId = estudiante.Id,
                        ClaseId = clase.Id,
                       
                    };

                    await _claseEstudianteRepository.AddAsync(nuevaRelacion, cancellationToken);

                }
            }

            await _estudianteRepository.UpdateAsync(estudiante, cancellationToken);
            return _mapper.Map<EstudianteDto>(estudiante);
        }
    }
}
