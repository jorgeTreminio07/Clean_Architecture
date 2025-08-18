using Ardalis.Result;
using MediatR;
using MyApp.Application.Dtos.Estudiante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Estudiante
{
    public record AddEstudianteCommand(string Name, List<Guid>? clasesId) : IRequest<Result<EstudianteDto>> 
    {
    }
}
