using Ardalis.Result;
using MediatR;
using MyApp.Application.Commands.Clase;
using MyApp.Application.Commands.Rol;
using MyApp.Application.Specifications.Rol;
using MyApp.Application.Specifications.RolPermiso;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Rol
{
    public class DeleteRolCommandHandler : IRequestHandler<DeleteRolCommand, Result<Guid>>
    {
        private readonly IAsyncRepository<RolEntity> _rolRepository;
        private readonly IAsyncRepository<RolPermisoEntity> _rolPermisoRepository;

        public DeleteRolCommandHandler(IAsyncRepository<RolEntity> rolRepository, IAsyncRepository<RolPermisoEntity> rolPermisoRepository)
        {
            this._rolRepository = rolRepository;
            this._rolPermisoRepository = rolPermisoRepository;
        }
        public async Task<Result<Guid>> Handle(DeleteRolCommand request, CancellationToken cancellationToken)
        {
            var rol = await _rolRepository.FirstOrDefaultAsync(new GetRoleByIdWithUsers(request.Id), cancellationToken);
            if(rol is null)
            {
                return Result.NotFound("No existe rol");
            }
            
            if(rol.UserEntities.Any())
            {
                return Result.Invalid(new ValidationError("Rol", "No se puede eliminar el rol porque tiene usuarios asociados activos."));
            }

            rol.IsDeleted = true;
            var relacionesActuales = await _rolPermisoRepository.ListAsync(new GetRolPermisoByIdRol(request.Id), cancellationToken);

            await _rolPermisoRepository.DeleteRangeAsync(relacionesActuales, cancellationToken);
            await _rolRepository.UpdateAsync(rol, cancellationToken);

            return Result.Success(rol.Id);
        }
    }
}
