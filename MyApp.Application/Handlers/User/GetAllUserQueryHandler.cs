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
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
    {
        private readonly IAsyncRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;

        public GetAllUserQueryHandler(IAsyncRepository<UserEntity> userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }
        public async Task<Result<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.ListAsync(new GetAllUserSpecifications(), cancellationToken);
            if (users is null || users.Count == 0)
            {
                return Result.NotFound();
            }

            var userDto = _mapper.Map<List<UserDto>>(users);
            return Result.Success(userDto);
        }
    }
}
