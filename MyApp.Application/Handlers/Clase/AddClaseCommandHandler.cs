using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Commands.Clase;
using MyApp.Application.Dtos.Clase;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Clase
{
    public class AddClaseCommandHandler : IRequestHandler<AddClaseCommand, Result<ClaseDto>>
    {
        private readonly IAsyncRepository<ClaseEntity> _claseRepository;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<EstudianteEntity> _estudianteRepository;

        public AddClaseCommandHandler(IAsyncRepository<ClaseEntity> claseRepository, IMapper mapper, IAsyncRepository<EstudianteEntity> estudianteRepository)
        {
            this._claseRepository = claseRepository;
            this._mapper = mapper;
            this._estudianteRepository = estudianteRepository;
        }
        public async Task<Result<ClaseDto>> Handle(AddClaseCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return Result.Invalid(new ValidationError("Name", "El nombre es obligatorio"));
            }

            var clase = _mapper.Map<ClaseEntity>(request);
            clase.Estudiantes = new List<ClaseEstudianteEntity>();

            //var clase = new ClaseEntity
            //{
            //    Name = request.Name,
            //    Estudiantes = new List<ClaseEstudianteEntity>()
            //};

            if (request.EstudiantesId != null && request.EstudiantesId.Any())
            {
                var allEstudiantes = await _estudianteRepository.ListAsync(cancellationToken);

                var estudiantesExist = allEstudiantes
                    .Where(c => request.EstudiantesId.Contains(c.Id))
                    .ToList();

                foreach (var estudiante in estudiantesExist)
                {
                    clase.Estudiantes.Add(new ClaseEstudianteEntity
                    {
                        ClaseId = clase.Id,
                        EstudianteId = estudiante.Id
                    });
                }
            }

            await _claseRepository.AddAsync(clase, cancellationToken);

            var dto = _mapper.Map<ClaseDto>(clase);
            return Result.Success(dto);
        }
    }
}
