using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Dtos.User;
using MyApp.Application.Queries.User;
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
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly IAsyncRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IAsyncRepository<UserEntity> userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }
        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(new GetUserByIdSpecification(request.Id), cancellationToken);
            if (user is null)
            {
                return Result.NotFound("No existe usuario");
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Result.Success(userDto);
        }
    }
}
