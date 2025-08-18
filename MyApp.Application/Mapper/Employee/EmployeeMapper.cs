using AutoMapper;
using MyApp.Application.Commands.Employee;
using MyApp.Application.Dtos.Employee;
using MyApp.Application.Dtos.Office;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Mapper.Employee
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<AddEmployeeCommand, EmployeeEntity>();
            CreateMap<UpdateEmployeeCommand, EmployeeEntity>();
            CreateMap<EmployeeEntity, EmployeeDto>();
            CreateMap<EmployeeEntity, EmployeeDtoWithoutOffice>();
        }
    }
}
