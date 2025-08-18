using Ardalis.Result;
using MediatR;
using MyApp.Application.Dtos.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Office
{
    public record AddOfficeCommand(string Name) : IRequest<Result<OfficeDto>>
    {
    }
}
