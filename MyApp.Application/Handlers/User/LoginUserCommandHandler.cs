using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Commands.User;
using MyApp.Application.Dtos.User;
using MyApp.Application.Interface.Security;
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
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginDto>>
    {
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAsyncRepository<UserEntity> _userRepository;
        public LoginUserCommandHandler(IMapper mapper, IJwtService jwtService, IPasswordHasher passwordHasher, IAsyncRepository<UserEntity> userRepository)
        {
            this._mapper = mapper;
            this._jwtService = jwtService;
            this._passwordHasher = passwordHasher;
            this._userRepository = userRepository;
        }
        public async Task<Result<LoginDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                return Result.Invalid(new ValidationError("request", "reques null"));
            }

            var user = await _userRepository.FirstOrDefaultAsync(new GetUserByEmailSpecification(request.Email), cancellationToken);
            
            if(user == null)
            {
                return Result.NotFound("User not exist");
            }

            bool isPassword = _passwordHasher.VerifyPassword(request.Password, user.Password);

            if(!isPassword)
            {
                return Result.Invalid(new ValidationError("Password", "invalid password"));
            }

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.RolId.ToString());

            var userDto = _mapper.Map<LoginDto>(user);
            userDto.Token = token;

            return Result.Success(userDto);


        }
    }
}
