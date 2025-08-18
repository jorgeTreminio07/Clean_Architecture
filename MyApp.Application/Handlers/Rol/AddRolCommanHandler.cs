using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Commands.Rol;
using MyApp.Application.Dtos.Rol;
using MyApp.Application.Specifications.Rol;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Rol
{
    public class AddRolCommanHandler : IRequestHandler<AddRolCommand, Result<RolDto>>
    {
        private readonly IAsyncRepository<RolEntity> _rolRespository;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<PermisosEntity> _permisoRepository;


        public AddRolCommanHandler(IAsyncRepository<RolEntity> rolRespository, IMapper mapper, IAsyncRepository<PermisosEntity> permisoRepository)
        {
            this._rolRespository = rolRespository;
            this._mapper = mapper;
            this._permisoRepository = permisoRepository;
        }

        public async Task<Result<RolDto>> Handle(AddRolCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result.Invalid(new ValidationError("request", "request is null"));
            }

            var existRol = await _rolRespository.FirstOrDefaultAsync(new GetRolByNameSpecification(request.Name), cancellationToken);
            if(existRol != null)
            {
                return Result.Invalid(new ValidationError("RolExist", "Rol Already Exist"));
            }

            var rolMap = _mapper.Map<RolEntity>(request);

            //Buscar permisos por nombre
            if(request.NamesPermisos is not null)
            {
                var permisos = await _permisoRepository.ListAsync(cancellationToken);
                var permisosAsociados = permisos
                    .Where(p => request.NamesPermisos.Contains(p.Name))
                    .Select(p => new RolPermisoEntity
                    {
                        PermisoId = p.Id,
                        RolId = rolMap.Id
                    }).ToList();

                rolMap.Permisos = permisosAsociados;
            }
            
            await _rolRespository.AddAsync(rolMap, cancellationToken);

            var rolDto = _mapper.Map<RolDto>(rolMap);
            
            return Result.Success(rolDto);
        }
    }
}
