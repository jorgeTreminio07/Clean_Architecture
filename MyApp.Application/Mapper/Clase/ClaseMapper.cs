using AutoMapper;
using MyApp.Application.Commands.Clase;
using MyApp.Application.Dtos.Clase;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Mapper.Clase
{
    public class ClaseMapper : Profile
    {
        public ClaseMapper()
        {
            //CreateMap<UpdateClaseCommand, ClaseEntity>();
            CreateMap<AddClaseCommand, ClaseEntity>();

            CreateMap<ClaseEntity, ClaseDto>()
                .ForMember(dest => dest.Estudiantes, opt => opt.MapFrom(src =>
                src.Estudiantes.Select(e => e.Estudiantes)));

            CreateMap<ClaseEntity,ClaseWithoutEstudiantesDto>();
        }
    }
}
