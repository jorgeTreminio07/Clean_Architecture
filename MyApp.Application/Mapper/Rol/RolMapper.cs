using AutoMapper;
using MyApp.Application.Commands.Rol;
using MyApp.Application.Dtos.Rol;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Mapper.Rol
{
    public class RolMapper : Profile
    {
        public RolMapper()
        {

            CreateMap<RolEntity, RolDto>()
            .ForMember(dest => dest.Permisos, opt => opt.MapFrom(src =>
                     src.Permisos.Select(rp => rp.Permisos!.Name).ToList()
            ));


            CreateMap<AddRolCommand, RolEntity>()
                .ForMember(dest => dest.Permisos, opt => opt.Ignore());

            CreateMap<UpdateRolCommand, RolEntity>()
                .ForMember(dest => dest.Permisos, opt => opt.Ignore());

            //CreateMap<RolEntity, RolDto>();
            CreateMap<RolEntity, RolForAuthDto>();
            CreateMap<RolEntity, RolWithUsersDto>()
                .ForMember(dest => dest.Permisos, opt => opt.MapFrom(src =>
                     src.Permisos.Select(rp => rp.Permisos!.Name).ToList()
            ));
        }
    }
}
