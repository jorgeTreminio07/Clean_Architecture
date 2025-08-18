using Ardalis.Result;
using MediatR;
using MyApp.Application.Commands.Estudiante;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Estudiante
{
    public class DeleteEstudianteCommandHandler : IRequestHandler<DeleteEstudianteCommand, Result<Guid>>
    {
        private readonly IAsyncRepository<EstudianteEntity> _estudianteRepository;

        public DeleteEstudianteCommandHandler(IAsyncRepository<EstudianteEntity> estudianteRepository)
        {
            this._estudianteRepository = estudianteRepository;
        }
        public async Task<Result<Guid>> Handle(DeleteEstudianteCommand request, CancellationToken cancellationToken)
        {
            var est = await _estudianteRepository.GetByIdAsync(request.Id, cancellationToken);
            if (est == null)
            {
                return Result<Guid>.NotFound($"Estudiante con ID '{request.Id}' no encontrado.");
            }

            if(est.IsDeleted)
            {
                return Result<Guid>.Invalid(new List<ValidationError> {
                    new("Estudiante", "El estudiante ya esta eliminado.")
                });
            }

            est.IsDeleted = true;
            await _estudianteRepository.UpdateAsync(est, cancellationToken);

            return Result.Success(est.Id);
        }
    }
}
