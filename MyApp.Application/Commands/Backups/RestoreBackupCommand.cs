using Ardalis.Result;
using MediatR;
using MyApp.Application.Dtos.Backups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Backups
{
    public record RestoreBackupCommand(Guid Id) : IRequest<Result<BackupDto>>
    {
    }
}
