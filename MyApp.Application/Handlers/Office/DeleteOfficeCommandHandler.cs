using Ardalis.Result;
using MediatR;
using MyApp.Application.Commands.Office;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Office
{
    public class DeleteOfficeCommandHandler : IRequestHandler<DeleteOfficeCommand, Result<Guid>>
    {
        private readonly IAsyncRepository<OfficeEntity> _officeRepository;

        public DeleteOfficeCommandHandler(IAsyncRepository<OfficeEntity> officeRepository)
        {
            this._officeRepository = officeRepository;
        }

        public async Task<Result<Guid>> Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
        {
           if (request == null)
           {
                return Result.Invalid(new ValidationError("request", "Request is null"));
           }

            var office = await _officeRepository.GetByIdAsync(request.Id, cancellationToken);
            if(office == null)
            {
                return Result<Guid>.NotFound($"Office con ID '{request.Id}' no encontrado.");
            }

            if (office.IsDeleted)
            {
                return Result<Guid>.NotFound($"Office con ID '{request.Id}' ya esta eliminado");
            }

            office.IsDeleted = true;

            await _officeRepository.UpdateAsync(office, cancellationToken);

            return Result<Guid>.Success(office.Id);
        }
    }
}
