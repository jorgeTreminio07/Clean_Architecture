using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Commands.User;
using MyApp.Application.Dtos.User;
using MyApp.Application.Interface.Archivos;
using MyApp.Application.Interface.Security;
using MyApp.Application.Specifications.Rol;
using MyApp.Application.Specifications.User;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.User
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<AuthDto>>
    {
        private readonly IAsyncRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAsyncRepository<RolEntity> _rolRepository;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        private const string contenedor = "UserAvatar";

        public UpdateUserCommandHandler(IAsyncRepository<UserEntity> userRepository, IMapper mapper, IPasswordHasher passwordHasher, IAsyncRepository<RolEntity> rolRepository, IAlmacenadorArchivos almacenadorArchivos)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._passwordHasher = passwordHasher;
            this._rolRepository = rolRepository;
            this._almacenadorArchivos = almacenadorArchivos;
        }
        public async Task<Result<AuthDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(new GetUserByIdSpecification(request.Id), cancellationToken);
            if (user == null)
            {
                return Result.NotFound("User not exist");
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                user.Name = request.Name;
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                user.Email = request.Email;
            }

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.Password = _passwordHasher.HashPassword(request.Password);
            }

            var rol = await _rolRepository.FirstOrDefaultAsync(new GetRolByIdSpecification(request.RolId), cancellationToken);
            //var rol = await _rolRepository.GetByIdAsync(request.RolId, cancellationToken);
            if (rol == null)
            {
                return Result.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "rol not exists",}
                });
            }

            if (request.RolId != Guid.Empty)
            {
                user.RolId = request.RolId;
            }

            if(request.Avatar != null)
            {
                var url = await _almacenadorArchivos.Editar(user.Avatar, contenedor, request.Avatar);
                user.Avatar = url;
            }

            await _userRepository.UpdateAsync(user, cancellationToken);

            var userDto = _mapper.Map<AuthDto>(user);
            return Result.Success(userDto);

        }
    }
}
