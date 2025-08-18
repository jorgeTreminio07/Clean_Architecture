using AutoMapper;
using MyApp.Application.Commands.User;
using MyApp.Application.Dtos.User;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Mapper.User
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<AddUserCommand, UserEntity>();
            CreateMap<UserEntity, AuthDto>();
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserEntity, LoginDto>();

            CreateMap<UserEntity, UserWithoutRolDto>();
        }
    }
}
