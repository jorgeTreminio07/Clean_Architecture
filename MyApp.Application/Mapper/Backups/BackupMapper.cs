using AutoMapper;
using MyApp.Application.Dtos.Backups;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Mapper.Backups
{
    public class BackupMapper : Profile
    {
        public BackupMapper()
        {
            CreateMap<BackupEntity, BackupDto>();
        }
    }
}
