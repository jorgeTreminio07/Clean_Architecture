using AutoMapper;
using MyApp.Application.Commands.Office;
using MyApp.Application.Dtos.Office;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Mapper.Office
{
    public class OfficeMappers : Profile
    {
        public OfficeMappers()
        {
            CreateMap<AddOfficeCommand, OfficeEntity>();
            CreateMap<OfficeEntity, OfficeDto>();
            CreateMap<OfficeEntity, OfficeDtoWithEmployees>();
            
        }
    }
}
