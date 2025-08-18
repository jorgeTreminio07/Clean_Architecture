using Ardalis.Result;
using MediatR;
using MyApp.Application.Dtos.Backups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Queries.Backups
{
    public record GetAllBackupsQuery : IRequest<Result<List<BackupDto>>>
    {
    }
}
