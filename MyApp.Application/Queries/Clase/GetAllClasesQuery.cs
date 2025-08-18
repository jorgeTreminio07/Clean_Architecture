using Ardalis.Result;
using MediatR;
using MyApp.Application.Dtos.Clase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Queries.Clase
{
    public record GetAllClasesQuery : IRequest<Result<List<ClaseDto>>>
    {
    }
}
