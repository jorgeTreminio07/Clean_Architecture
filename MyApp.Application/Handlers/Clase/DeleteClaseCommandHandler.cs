using Ardalis.Result;
using MediatR;
using MyApp.Application.Commands.Clase;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Clase
{
    public class DeleteClaseCommandHandler : IRequestHandler<DeleteClaseCommand, Result<Guid>>
    {
        private readonly IAsyncRepository<ClaseEntity> _claseRepository;

        public DeleteClaseCommandHandler(IAsyncRepository<ClaseEntity> claseRepository)
        {
            this._claseRepository = claseRepository;
        }
        public async Task<Result<Guid>> Handle(DeleteClaseCommand request, CancellationToken cancellationToken)
        {
            var clase = await _claseRepository.GetByIdAsync(request.Id, cancellationToken);

            if (clase == null)
            {
                return Result.NotFound("Id no existe");
            }

            if (clase.IsDeleted)
            {
                return Result.Invalid(new ValidationError("request", "id ya esta eliminado"));
            }

            clase.IsDeleted = true;

            await _claseRepository.UpdateAsync(clase);
            return Result.Success(clase.Id);

        }
    }
}
