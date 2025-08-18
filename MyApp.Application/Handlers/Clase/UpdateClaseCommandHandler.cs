using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Commands.Clase;
using MyApp.Application.Dtos.Clase;
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
    public class UpdateClaseCommandHandler : IRequestHandler<UpdateClaseCommand, Result<ClaseDto>>
    {
        private readonly IAsyncRepository<ClaseEntity> _claseRepository;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<EstudianteEntity> _estudianteRepository;
        private readonly IAsyncRepository<ClaseEstudianteEntity> _claseEstudianteRepository;

        public UpdateClaseCommandHandler(IAsyncRepository<ClaseEntity> claseRepository, IMapper mapper, IAsyncRepository<EstudianteEntity> estudianteRepository, IAsyncRepository<ClaseEstudianteEntity> claseEstudianteRepository)
        {
            this._claseRepository = claseRepository;
            this._mapper = mapper;
            this._estudianteRepository = estudianteRepository;
            this._claseEstudianteRepository = claseEstudianteRepository;
        }
        public async Task<Result<ClaseDto>> Handle(UpdateClaseCommand request, CancellationToken cancellationToken)
        {
            var clase = await _claseRepository.GetByIdAsync(request.Id, cancellationToken);
         
            if (clase == null)
            {
                return Result.NotFound($"No se encontró la clase con ID {request.Id}");
            }
          
            clase.Name = request.Name;

            // Obtener relaciones actuales
            var relacionesActuales = await _claseEstudianteRepository
                .ListAsync(new GetByClaseIdSpecification(clase.Id), cancellationToken);

            var idsEstudiantesActuales = relacionesActuales.Select(r => r.EstudianteId).ToHashSet();
            var idsNuevos = (request.EstudiantesId ?? new List<Guid>()).ToHashSet();

            // 🔴 1. Eliminar relaciones que ya no están
            var relacionesAEliminar = relacionesActuales
                .Where(r => !idsNuevos.Contains(r.EstudianteId))
                .ToList();

            if (relacionesAEliminar.Any())
            {
                await _claseEstudianteRepository.DeleteRangeAsync(relacionesAEliminar, cancellationToken);
            }

          

            // 🟢 2. Agregar nuevas relaciones
            var idsAAgregar = idsNuevos
                .Where(id => !idsEstudiantesActuales.Contains(id))
                .ToList();

            //agregar sin sobrecargar la db
            //var nuevaRelacion = idsAAgregar.Select(id =>

            //    new ClaseEstudianteEntity
            //    {
            //        ClaseId = clase.Id,
            //        EstudianteId = id,
            //    }
            //).ToList();

            //if (nuevaRelacion.Any())
            //{
            //    await _claseEstudianteRepository.AddRangeAsync(nuevaRelacion, cancellationToken);
            //}


            foreach (var estudianteId in idsAAgregar)
            {
                var estudiante = await _estudianteRepository.GetByIdAsync(estudianteId, cancellationToken);
                if (estudiante != null)
                {
                    var nuevaRelacion = new ClaseEstudianteEntity
                    {
                        ClaseId = clase.Id,
                        EstudianteId = estudiante.Id
                    };

                    await _claseEstudianteRepository.AddAsync(nuevaRelacion, cancellationToken);
                }
            }

            
            // Actualizar la clase (nombre u otro campo)
            await _claseRepository.UpdateAsync(clase, cancellationToken);

            var claseDto = _mapper.Map<ClaseDto>(clase);
            return Result.Success(claseDto);
        }
    }
}
