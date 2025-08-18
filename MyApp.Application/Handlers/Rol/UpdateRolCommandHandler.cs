using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Commands.Rol;
using MyApp.Application.Dtos.Rol;
using MyApp.Application.Specifications.Clase;
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
    public class UpdateRolCommandHandler : IRequestHandler<UpdateRolCommand, Result<RolDto>>
    {
        private readonly IAsyncRepository<RolEntity> _rolRespository;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<PermisosEntity> _permisoRepository;
        private readonly IAsyncRepository<RolPermisoEntity> _rolPermisoRepository;
        public UpdateRolCommandHandler(IAsyncRepository<RolEntity> rolRespository, IAsyncRepository<PermisosEntity> permisoRepository, IAsyncRepository<RolPermisoEntity> rolPermisoRepository, IMapper mapper)
        {
            _rolRespository = rolRespository;
            _permisoRepository = permisoRepository;
            _rolPermisoRepository = rolPermisoRepository;
            _mapper = mapper;
        }

        public async Task<Result<RolDto>> Handle(UpdateRolCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                return Result.Invalid(new ValidationError("request", "request is null"));
            }

            var rol = await _rolRespository.FirstOrDefaultAsync(new GetRolByIdSpecification(request.Id), cancellationToken);
            if(rol == null)
            {
                return Result.NotFound("Rol Not Exist");
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                rol.Name = request.Name;
            }
            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                rol.Description = request.Description;
            }

            //Buscar permisos por nombre
            if (request.NamesPermisos is not null)
            {
                var permisos = await _permisoRepository.ListAsync(cancellationToken);
                var permisosAsociados = permisos
                    .Where(p => request.NamesPermisos.Contains(p.Name))
                    .Select(p => new RolPermisoEntity
                    {
                        PermisoId = p.Id,
                        RolId = rol.Id
                    }).ToList();

                rol.Permisos = permisosAsociados;
            }

            var relacionesActuales = await _rolPermisoRepository.ListAsync(new GetRolPermisoByIdRol(request.Id), cancellationToken);

            await _rolPermisoRepository.DeleteRangeAsync(relacionesActuales, cancellationToken);
            await _rolRespository.UpdateAsync(rol, cancellationToken);

            var rolDto = _mapper.Map<RolDto>(rol);
            return Result.Success(rolDto);
        }
    }
}
