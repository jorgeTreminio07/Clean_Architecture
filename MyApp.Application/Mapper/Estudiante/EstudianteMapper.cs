using AutoMapper;
using MyApp.Application.Commands.Estudiante;
using MyApp.Application.Dtos.Estudiante;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Mapper.Estudiante
{
    public class EstudianteMapper : Profile
    {
        public EstudianteMapper()
        {
            CreateMap<AddEstudianteCommand, EstudianteEntity>();
            CreateMap<EstudianteEntity, EstudianteDto>()
                .ForMember(dest => dest.Clases, opt => opt.MapFrom(src => src.Clases.Select(a => a.Clases)));
            CreateMap<EstudianteEntity, EstudianteWithoutClasesDto>();
        }
    }
}
