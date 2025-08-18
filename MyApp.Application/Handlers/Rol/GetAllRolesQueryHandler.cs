using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Dtos.Rol;
using MyApp.Application.Queries.Rol;
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
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolQuery, Result<List<RolWithUsersDto>>>
    {
        private readonly IAsyncRepository<RolEntity> _rolRepository;
        private readonly IMapper _mapper;

        public GetAllRolesQueryHandler(IAsyncRepository<RolEntity> rolRepository, IMapper mapper)
        {
            this._rolRepository = rolRepository;
            this._mapper = mapper;
        }
        public async Task<Result<List<RolWithUsersDto>>> Handle(GetAllRolQuery request, CancellationToken cancellationToken)
        {
            var rol = await _rolRepository.ListAsync(new GetAllRolesSpecifications() ,cancellationToken);
            if (rol is null || rol.Count == 0)
            {
                return Result.NotFound();
            }

            var rolDto = _mapper.Map<List<RolWithUsersDto>>(rol);
            return Result.Success(rolDto);

        }
    }
}
