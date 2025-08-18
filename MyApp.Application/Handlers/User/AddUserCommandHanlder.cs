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
    public class AddUserCommandHanlder : IRequestHandler<AddUserCommand, Result<AuthDto>>
    {
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAsyncRepository<UserEntity> _userRepository;
        private readonly IAsyncRepository<RolEntity> _rolRepository;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        private const string contenedor = "UserAvatar";

        public AddUserCommandHanlder(IMapper mapper, IJwtService jwtService, IPasswordHasher passwordHasher, IAsyncRepository<UserEntity> userRepository, IAsyncRepository<RolEntity> rolRepository, IAlmacenadorArchivos almacenadorArchivos)
        {
            this._mapper = mapper;
            this._jwtService = jwtService;
            this._passwordHasher = passwordHasher;
            this._userRepository = userRepository;
            this._rolRepository = rolRepository;
            this._almacenadorArchivos = almacenadorArchivos;
        }
        public async Task<Result<AuthDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UserEntity>(request);

            if (user == null)
            {
                return Result.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Request is null",}
                });
            }

            var emailUser = await _userRepository.FirstOrDefaultAsync(new GetUserByEmailSpecification(request.Email), cancellationToken);
            if (emailUser != null)
            {
                return Result.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Email already exists",}
                });
            }

            var rol = await _rolRepository.FirstOrDefaultAsync(new GetRolByIdSpecification(request.RolId), cancellationToken);
            //var rol = await _rolRepository.GetByIdAsync(request.RolId, cancellationToken);
            if (rol == null)
            {
                return Result.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "rol not exists",}
                });
            }


            user.Password = _passwordHasher.HashPassword(request.Password);
            user.Rol = rol;

            if(request.Avatar != null)
            {
                user.Avatar = await _almacenadorArchivos.Almacenar(contenedor, request.Avatar);
            }

            await _userRepository.AddAsync(user, cancellationToken);


            var userDto = _mapper.Map<AuthDto>(user);
            userDto.Token = _jwtService.GenerateToken(user.Id.ToString(), user.RolId.ToString());

            return Result.Success(userDto);
        }
    }
}
